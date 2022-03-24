using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] Oxygen_bar oxygenBar;
    [SerializeField] TextMeshProUGUI AmmoCount, Score, TargetScore;
    [SerializeField] float currentHealth;
    [SerializeField] int currentAmmo;
    [SerializeField] float currentOxygen;
    [SerializeField] private AmmoCounterSO ammoCounter;
    [SerializeField] private OverallScoreSO OverallScore;
    private PlayerController pc;
    private GunController gc;
    private GameObject ourPlayer;
    private GameObject gun;

    void Awake()
    {
        OverallScore.overallScore = 0;
        ourPlayer = GameObject.FindGameObjectWithTag("Player");
        gun = GameObject.FindGameObjectWithTag("Gun");
        pc = ourPlayer.GetComponent<PlayerController>();
        gc = gun.GetComponent<GunController>();
        currentHealth = pc.getHealth();
        currentOxygen = pc.getOxygen();
        currentAmmo = ammoCounter.ammoCount;
        healthBar.SetMaxHealth(100);
        oxygenBar.SetMaxHealth(100);
        AmmoCount.text = currentAmmo.ToString();
    }

    void Update()
    {
        TargetScore.text = "Target Score: " + pc.getTargetScore();
        Score.text = "Score: " + OverallScore.overallScore.ToString();
        currentHealth = pc.getHealth();
        healthBar.SetHealth(currentHealth);
        currentOxygen = pc.getOxygen();
        oxygenBar.SetHealth(currentOxygen);
        currentAmmo = ammoCounter.ammoCount;
        AmmoCount.text = currentAmmo.ToString();
    }
}
