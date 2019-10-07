using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFromBlackHoleMass : MonoBehaviour
{
    protected Text text;
    protected GameController gameController;

    protected void Awake() {
        text = GetComponent<Text>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    protected void Update()
    {
        if(gameController.blackHole == null) return;
        text.text = gameController.blackHole.Mass.ToString("0");
    }
}
