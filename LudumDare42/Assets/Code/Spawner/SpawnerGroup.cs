using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGroup : MonoBehaviour
{
    public bool IsActive = true;
    bool _previousActiveValue;

    public void SetActive(bool flag)
    {
        BaseSpawner[] spawners = gameObject.GetComponentsInChildren<BaseSpawner>();

        foreach (var s in spawners)
            s.isActive = flag;
    }

    void Start()
    {
        SetActive(IsActive);
    }

    void Update()
    {
        if (IsActive != _previousActiveValue)
            SetActive(IsActive);

        _previousActiveValue = IsActive;
    }
}
