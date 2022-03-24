using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenItem : MonoBehaviour
{
    [SerializeField]
    [Range(10, 20)]
    [Tooltip("Oxygen gained when player collects oxygen loot")]
    private int oxygenAdded = 10;
    private PlayerController pc;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            pc.addOxygen(oxygenAdded);
            Destroy(this.gameObject);
        }
    }
     void Update() {
          transform.Rotate(0,0,1f);

    }
}
