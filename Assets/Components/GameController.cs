using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameController : MonoBehaviour
{
    public float gravitationConstant = 2;
    public int particlesToMerge = 5;
    public int scaleMultiplier = 3;

    public Gradient[] tierGradients;
    public RuntimeAnimatorController[] tierAnimation;
}
