using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFromUnspentBlackHoleMass : MonoBehaviour
{
    protected Text text;
    protected GameController gameController;

    protected void Awake() {
        text = GetComponent<Text>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    protected void Update()
    {
        text.text = gameController.blackHole.UnspentMass.ToString("0");
    }
}
