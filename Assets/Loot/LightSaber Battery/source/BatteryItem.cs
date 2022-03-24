using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem : MonoBehaviour
{
    [SerializeField]
    [Range(10, 20)]
    [Tooltip("Percentage of battery charged")]
    private int chargeAdded = 100;
    [SerializeField] BatteryTrackerSO chargeTracker;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            chargeTracker.charge += chargeAdded;
            Destroy(this.gameObject);
        }
    }

    void Update() {
        transform.Rotate(0, 1f, 0);
    }
}
