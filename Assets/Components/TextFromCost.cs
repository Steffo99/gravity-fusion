using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFromCost : MonoBehaviour
{
    protected Text text;

    protected void Awake() {
        text = GetComponent<Text>();
    }

    public void DisplayCost(float cost) {
        if(cost > 0) {
            text.text = cost.ToString("0");
        }
        else {
            text.text = "";
        }
    }
}
