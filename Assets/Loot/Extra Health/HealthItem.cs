using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Health gained when player collects health loot")]
    private int healthAdded = 10;
    PlayerController pc;

    void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            pc.addHealth(healthAdded);
            Destroy(this.gameObject);
        }
    }
      void Update() {
          transform.Rotate(0,1f,0);

    }
}
