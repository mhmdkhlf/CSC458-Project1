 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleArea : MonoBehaviour
{
    [SerializeField] private GameObject monsterToSpawn;
    private AudioSource myAudioSource;
    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            myAudioSource.Play();
            StartCoroutine(instantiateMonster());
        }
    }
    IEnumerator instantiateMonster() {
	    yield return new WaitForSeconds(2);
  	    Instantiate(monsterToSpawn, transform.position, Quaternion.identity);
    }
}
