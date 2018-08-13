using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnActivator : MonoBehaviour {

	public FixedRateSpawner spawner;

	// Use this for initialization
	void Start () {
		spawner = GetComponentInParent<FixedRateSpawner>();
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag("Player")) {
			spawner.spawnerActive = true;
		}
	}
}
