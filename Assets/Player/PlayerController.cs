using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject shortRangeWeapon, longRangeWeapon, crossHair;
    private bool isGunActive;
    [Range(20, 50)]
    [SerializeField] private int targetScore = 40;
    [SerializeField] OverallScoreSO OverallScore;
    private const float OXYGEN_UPPER_LIMT = 100, OXYGEN_LOWER_LIMT = 0;
    private const float HEALTH_UPPER_LIMT = 100, HEALTH_LOWER_LIMT = 0;
    private float playerHealth = 100f;
    private float playerOxygen = 100f;
    private AudioSource winningAudio;
    private const float GRADUAL_OXYGEN_LOST = 0.9f;

    void Start()
    {
        winningAudio = transform.GetChild(0).GetComponent<AudioSource>();
        shortRangeWeapon.SetActive(true);
        longRangeWeapon.SetActive(false);
        crossHair.SetActive(false);
        isGunActive = false;
        OverallScore.TargetScore = targetScore;
    }

    void Update()
    {
        if (isDead()){
            SceneManager.LoadScene(2);
        }
        if (OverallScore.overallScore >= targetScore){
            StartCoroutine(levelComplete());
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            isGunActive= !isGunActive;
            if (isGunActive){
                shortRangeWeapon.SetActive(false);
                longRangeWeapon.SetActive(true);
                crossHair.SetActive(true);
            }
            else{
                shortRangeWeapon.SetActive(true);
                longRangeWeapon.SetActive(false);
                crossHair.SetActive(false);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isGunActive)
                longRangeWeapon.GetComponent<GunController>().shoot();
            else
                shortRangeWeapon.GetComponent<LightSaberController>().shoot();
        }
        decrementOxygen(GRADUAL_OXYGEN_LOST * Time.deltaTime);
    }

    public float getHealth(){
        return playerHealth;
    }
    public void decrementHealth(float damage){
        playerHealth = Mathf.Clamp(playerHealth - damage, HEALTH_LOWER_LIMT, HEALTH_UPPER_LIMT);
    }
    public void addHealth(float addedHealth){
        playerHealth = Mathf.Clamp(playerHealth + addedHealth, HEALTH_LOWER_LIMT, HEALTH_UPPER_LIMT);
    }

    public float getOxygen(){
        return playerOxygen;
    }
    private void decrementOxygen(float oxygenLost){
        playerOxygen = Mathf.Clamp(playerOxygen - oxygenLost, OXYGEN_LOWER_LIMT, OXYGEN_UPPER_LIMT);
    }
    public void addOxygen(float addedOxygen){
        playerOxygen = Mathf.Clamp(playerOxygen + addedOxygen, OXYGEN_LOWER_LIMT, OXYGEN_UPPER_LIMT);
    }

    private bool isDead(){
        return playerHealth == 0 || playerOxygen == 0;
    }

    public int getTargetScore(){
        return targetScore;
    }
    IEnumerator levelComplete()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(4);
    }
}
