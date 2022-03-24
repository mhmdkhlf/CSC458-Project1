using UnityEngine;
using UnityEngine.AI;
enum Enemy
{
  smallEnemy,
  mediumEnemy,
  Monster
}
public class EnemyController: MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private float startAnimation;
    private PlayerController pc;
    [SerializeField] Enemy enemyType;
    [SerializeField] OverallScoreSO OverallScore;
    private Animator animator;
    private bool isprovoked = false;
    [SerializeField] GameObject oxygenLoot, healthLoot, ammoLoot, batteryLoot;

    [Range(50f, 150f)]
    [Tooltip("Distance at which enemy can detect player")]
    [SerializeField] private float attackRange = 70f;
    [Range(1, 20)]
    [SerializeField] private int hitDamage = 2;
    [Range(1, 20)]
    [SerializeField] private int health = 1;
    private AudioSource audioDeath;
    private bool killed = false;
    private void Awake()
    {
        audioDeath = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("is_attack", false);
        startAnimation = agent.stoppingDistance;
    }

    private void Update()
    {
        if (isDead() && !killed){
            kill();
            killed = true;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            isprovoked = true;
        }
        if (isprovoked)
        {
            animator.SetBool("is_walk",true);
            ChasePlayer();
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= startAnimation)
        {
            animator.SetBool("is_attack", true);
            pc.decrementHealth(hitDamage * Time.deltaTime);
        }
        else
        {
            animator.SetBool("is_attack", false);
        }
    }

    public void damage(int d){
        health = Mathf.Clamp(health - d, 0, health);
    }

    public bool isDead() {
        return health == 0;
    }

    private void kill() {
        agent.isStopped = true;
        audioDeath.Play();
        animator.SetBool("is_dead", true);
        addScore();
        generateLoot();
        Destroy(gameObject, 2f);
    }

    private void generateLoot(){
        int nbOfItemsRewarded;
        if (enemyType == Enemy.smallEnemy)
            nbOfItemsRewarded = 1;
        else if (enemyType == Enemy.mediumEnemy)
            nbOfItemsRewarded = 2;
        else
            nbOfItemsRewarded = 5;
        int choice;
        GameObject[] loot = {oxygenLoot, healthLoot, ammoLoot, batteryLoot};
        Vector3[] location = {transform.forward, -transform.forward, transform.right, -transform.right};
        for (int i=0; i<nbOfItemsRewarded; i++){
            choice = Random.Range(0, 4);
            GameObject toInstantiate = loot[choice];
            Instantiate(toInstantiate, transform.position + transform.up * 2 + location[choice], Quaternion.identity);
        }
    }
    private void addScore(){
        if (enemyType == Enemy.smallEnemy)
            OverallScore.overallScore += 2;
        else if (enemyType == Enemy.mediumEnemy)
            OverallScore.overallScore += 5;
        else
            OverallScore.overallScore += 10;
        if (OverallScore.overallScore >= OverallScore.TargetScore)
            OverallScore.overallScore = OverallScore.TargetScore;
    }

    public int getHealth(){
        return health;
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
}