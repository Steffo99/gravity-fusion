using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle))]
public class Hue : MonoBehaviour
{
    protected Gradient _possibleColors;

    protected Particle particle;
    protected Material material;
    protected Color color;

    protected void Awake() {
        particle = GetComponent<Particle>();
        material = new Material(Shader.Find("Custom/HSVRangeShader"));
    }

    public Gradient PossibleColors {
        get {
            return _possibleColors;
        }
        set {
            _possibleColors = value;
            color = _possibleColors.Evaluate(Random.value);
        }
    }

    public void RefreshColor() {
        Vector4 hsva = new Vector4(0, 0, particle.disappear.health - 1, 0);
        Color.RGBToHSV(color, out hsva.x, out _, out _);
        material.SetVector("_HSVAAdjust", hsva);
    }

    protected void Start() {
        particle.mainRenderer.material = material;
        particle.auraRenderer.material = material;
        particle.detailsRenderer.material = material;
    }

    protected void Update() {
        RefreshColor();
    }
}
