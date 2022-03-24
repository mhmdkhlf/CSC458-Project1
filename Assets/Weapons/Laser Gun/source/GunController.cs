using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [Range(0.4f, 0.8f)]
    [Tooltip("time in seconds between two shots")]
    [SerializeField] private float fireRate = 0.5f;
    [Range(15, 40)]
    [Tooltip("Initial ammo count in start of game")]
    [SerializeField] private int initialAmmoCount = 20;
    [SerializeField] private AmmoCounterSO ammoCounter;
    private const int MAX_AMMO_COUNT = 100;
    [SerializeField] private AudioClip emptyGunSound;
    private AudioSource myAudioSource;
    private float lastShotTime = 0f;
    private RaycastHit hitInfo;
    private Vector3 offset = new Vector3(0.5f, 0, 0.5f);

    void Awake(){
        myAudioSource = GetComponent<AudioSource>();
        ammoCounter.ammoCount = initialAmmoCount;
    }

    public void shoot(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Time.time >= lastShotTime + fireRate){
            if (ammoCounter.ammoCount < 1){
                myAudioSource.PlayOneShot(emptyGunSound);
                lastShotTime = Time.time;
            }
            else {
                Physics.Raycast(ray, out hitInfo);
                GameObject projectile = GameObject.Instantiate(laserPrefab, transform.parent.position, transform.parent.rotation) as GameObject;
                projectile.GetComponent<ShotBehavior>().setTarget(hitInfo.point);
                ammoCounter.ammoCount--;
                GameObject.Destroy(projectile, 2f);
                myAudioSource.Play();
                lastShotTime = Time.time;

                if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == "Enemy")
                {
                    EnemyController enemy = hitInfo.collider.gameObject.GetComponent<EnemyController>();
                    if (!enemy.isDead()){
                        enemy.damage(1);
                        enemy.ChasePlayer();
                    }
                }
            }
        }
    }
}
