using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpringManager : MonoBehaviour
{
    public Transform capsule1;
    public Transform capsule2;
    private Color yellow = Color.yellow;
    private Color red = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;
    }

    // Update the position and the color of the "spring"
    // The more stretched it is, the more redder
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, capsule1.transform.position);
        lineRenderer.SetPosition(1, capsule2.transform.position);

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(yellow, 0.0f), new GradientColorKey(red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        var color = gradient.Evaluate((capsule1.transform.position - capsule2.transform.position).magnitude / 10);
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
