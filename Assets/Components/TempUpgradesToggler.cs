using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUpgradesToggler : MonoBehaviour
{
    public GameObject upgrades;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            upgrades.SetActive(!upgrades.activeSelf);
        }
    }
}
