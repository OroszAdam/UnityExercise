using System.Collections;
using UnityEngine;

public class AnimateCamera : MonoBehaviour
{
    public Vector3 pos1; // Start position
    public Vector3 pos2; // Intermediate position
    public Vector3 pos3; // End position
    public float duration = 30f; // Duration of the movement along the curve
    private float timer = 0f;
    private bool forward = true; // Direction of movement
    public Transform objectToLookAt;
    void Update()
    {
        // Move camera along the curve back and forth
        MoveCameraAlongCurve();
    }

    // Move camera along the curve back and forth
    void MoveCameraAlongCurve()
    {
        timer += Time.deltaTime;

        // Calculate t parameter for Bezier curve
        float t = Mathf.Clamp01(timer / duration);

        // Check if movement direction needs to be reversed
        if (t >= 1f || t <= 0f)
        {
            forward = !forward;
            timer = 0f;
        }

        if (forward)
        {
            // Move forward along the curve
            transform.position = CalculateBezierPoint(t);
        }
        else
        {
            // Move backward along the curve
            transform.position = CalculateBezierPoint(1 - t);
        }
        // Make the camera always the chosen object
        transform.LookAt(objectToLookAt.transform);
    }

    // Calculate Bezier curve point using given t parameter
    Vector3 CalculateBezierPoint(float t)
    {
        // Bezier curve formula
        return Mathf.Pow(1 - t, 2) * pos1 + 2 * (1 - t) * t * pos2 + Mathf.Pow(t, 2) * pos3;
    }
}
