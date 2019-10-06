using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public enum UpgradeType {
    ANTIG, MATTER, FISSION
}

public class UpgradeButton : MonoBehaviour 
{
    public int cost;
    public int level;
    public UpgradeType type;

    public Sprite cantBuySprite;
    public Sprite canBuySprite;
    public Sprite hoveredSprite;
    public Sprite boughtSprite;

    protected bool CanBeBought {
        get {
            return gameController.blackHole.UnspentMass >= cost;
        }
    }

    protected bool HasBeenBought {
        get {
            switch(type) {
                case UpgradeType.ANTIG: return gameController.LevelAntig >= level;
                case UpgradeType.MATTER: return gameController.LevelMatter >= level;
                case UpgradeType.FISSION: return gameController.LevelFission >= level;
            }
            //ok c#
            return false;
        }
    }

    protected GameController gameController;
    protected Image image;

    protected void Awake() {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        image = GetComponent<Image>();
    }

    protected void Start() {
        RefreshUpgrades();
    }

    public void OnPointerEnter() {
        if(!HasBeenBought && CanBeBought) {
            image.sprite = hoveredSprite;
            gameController.canvas.BroadcastMessage("DisplayCost", cost);
        }
    }

    public void OnPointerExit() {
        if(!HasBeenBought && CanBeBought) {
            image.sprite = canBuySprite;
            gameController.canvas.BroadcastMessage("DisplayCost", -1f);
        }
    }
    
    public void OnPointerClick() {
        if(!HasBeenBought && CanBeBought) {
            gameController.blackHole.UnspentMass -= cost;
            switch(type) {
                case UpgradeType.ANTIG: gameController.LevelAntig = level; break;
                case UpgradeType.MATTER: gameController.LevelMatter = level; break;
                case UpgradeType.FISSION: gameController.LevelFission = level; break;
            }
            gameController.canvas.BroadcastMessage("RefreshUpgrades");
        }
    }

    public void RefreshUpgrades() {
        if(HasBeenBought) {
            image.sprite = boughtSprite;
        }
        else if(CanBeBought) {
            image.sprite = canBuySprite;
        }
        else {
            image.sprite = cantBuySprite;
        }
    }
    
}
