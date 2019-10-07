using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUpgradesToggler : MonoBehaviour
{
    public GameObject upgrades;

    protected void Start() {
        Debug.LogWarning("TempUpgradesToggler should not be used.");
    }

    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            upgrades.SetActive(!upgrades.activeSelf);
        }
    }
}
