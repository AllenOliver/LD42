using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOverTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(OverTime());
	}

    IEnumerator OverTime()
    {
        yield return new WaitForSeconds(1f);
        EZ_Pooling.EZ_PoolManager.Despawn(gameObject.transform);
    }
}
