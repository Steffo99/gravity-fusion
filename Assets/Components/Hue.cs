using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Particle))]
public class Hue : MonoBehaviour
{
    protected Gradient _possibleColors;

    protected Particle particle;
    protected Material material;

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
            Color = _possibleColors.Evaluate(Random.value);
        }
    }

    protected Color Color {
        get {
            Vector4 hsva = material.GetVector("_HSVAAdjust");
            return Color.HSVToRGB(hsva.x, hsva.y, hsva.z);
        }
        set {
            Vector4 hsva = new Vector4(0, 0, 0, 0);
            Color.RGBToHSV(value, out hsva.x, out _, out _);
            material.SetVector("_HSVAAdjust", hsva);
        }
    }

    protected void Start() {
        particle.mainRenderer.material = material;
        particle.auraRenderer.material = material;
        particle.detailsRenderer.material = material;
    }
}
