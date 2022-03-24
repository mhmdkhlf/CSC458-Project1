using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] private AmmoCounterSO ammoCounter;
    private const int MAX_AMMO_COUNT = 100;
    [SerializeField]
    private int addedAmmo = 20;

    void Update()
    {
        transform.Rotate(0, 1f, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            ammoCounter.ammoCount = Mathf.Clamp(ammoCounter.ammoCount + addedAmmo, 0, MAX_AMMO_COUNT);
            Destroy(this.gameObject);
        }
    }
}
