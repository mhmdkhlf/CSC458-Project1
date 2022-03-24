using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberController : MonoBehaviour
{
    private Animator animator;
    private bool isAnimationPlaying = false;
    private const int SWORD_LENGTH = 8;
    private float lastShotTime = 0f;
    private float fireRate = 0.7f;
    private RaycastHit hit;
    private bool isAnimationDone = true;
    [SerializeField]
    private GameObject collisionBurn;
    [SerializeField] BatteryTrackerSO chargeTracker;
    private const float CHARGE_UPPER_LIMT = 100, CHARGE_LOWER_LIMT = 10, GRADUAL_CHARGE_LOST = 3;
    private Renderer rend;
    private AudioSource audioSource;

    void Start()
    {
        chargeTracker.charge = 100f;
        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (isAnimationPlaying)
        {
            if(Physics.Raycast(transform.position, transform.right, out hit , SWORD_LENGTH) && hit.collider.gameObject.tag == "Enemy")
            {
                EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                GameObject explosion = (GameObject) Instantiate(collisionBurn, hit.point, Quaternion.identity);
                if (!enemy.isDead() && isAnimationDone)
                {
                    enemy.damage(10);
                    isAnimationDone = false;
                }
            }
        }
        decrementCharge(GRADUAL_CHARGE_LOST * Time.deltaTime);
        rend.materials[1].color = new Color(rend .material.color.r, rend .material.color.g, rend.material.color.b, chargeTracker.charge / 100);
    }

    public void shoot(){
        if (Time.time >= lastShotTime + fireRate && chargeTracker.charge != CHARGE_LOWER_LIMT){
            animator.Play("New Animation");
            StartCoroutine(updateBool());
            lastShotTime = Time.time;
        }
        if (chargeTracker.charge == CHARGE_LOWER_LIMT){
            audioSource.Play();
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * SWORD_LENGTH);
    }

    IEnumerator updateBool()
    {
        yield return new WaitForSeconds(0.2f);
        isAnimationPlaying = true;
        yield return new WaitForSeconds(1.2f);
        isAnimationDone = true;
        isAnimationPlaying = false;
    }

    public void addBattery(float addedCharge){
        chargeTracker.charge = Mathf.Clamp(chargeTracker.charge + addedCharge, CHARGE_LOWER_LIMT, CHARGE_UPPER_LIMT);
    }
    private void decrementCharge(float chargeLost){
        chargeTracker.charge = Mathf.Clamp(chargeTracker.charge - chargeLost, CHARGE_LOWER_LIMT, CHARGE_UPPER_LIMT);
    }
}
