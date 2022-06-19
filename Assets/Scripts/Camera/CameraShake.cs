using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraShake : MonoBehaviour
{
    public bool decreaseTrauma = true;
    public float decreaseTraumaRate = 0.1f;
    public float maxAngle = 10f;
    public float maxOffset = 1f;
    [SerializeField]
    private float _trauma;
    public float trauma
    {
        get
        {
            return _trauma;
        }
        set
        {
            _trauma = Mathf.Clamp01(value);
        }
    }
    public TraumaScalation traumaScalation;


    [SerializeField]
    private Transform pivot;
    private Camera cam;


    public enum TraumaScalation
    {
        Linear,
        Squared,
        Cubed
    }

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (pivot == null)
        {
            pivot = cam.transform.parent;
        }
    }

    private void Update()
    {
        float traumaScaled;
        switch (traumaScalation)
        {
            case TraumaScalation.Linear:
                traumaScaled = Mathf.Clamp01(trauma);
                break;
            case TraumaScalation.Squared:
                traumaScaled = Mathf.Clamp01(trauma * trauma);
                break;
            case TraumaScalation.Cubed:
                traumaScaled = Mathf.Clamp01(trauma * trauma * trauma);
                break;
            default:
                traumaScaled = Mathf.Clamp01(trauma);
                break;
        }
        cam.transform.position = pivot.position +
            (Vector3)new Vector2(
                Mathf.PerlinNoise(Time.time, 0),
                Mathf.PerlinNoise(Time.time + 1, 0)
            ) * maxOffset * traumaScaled;
        cam.transform.rotation = pivot.rotation;
        cam.transform.RotateAround(pivot.position, pivot.forward, Mathf.PerlinNoise(Time.time + 1, 0) * maxAngle * traumaScaled);


        if (decreaseTrauma)
        {
            trauma -= decreaseTraumaRate * Time.deltaTime;
        }
    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraShake cs = (CameraShake)target;
        if (GUILayout.Button("Max Shake"))
        {
            cs.trauma = 1;
        }
        if (GUILayout.Button("Small Shake"))
        {
            cs.trauma += 0.2f;
        }
        if (GUILayout.Button("Big Shake"))
        {
            cs.trauma += 0.4f;
        }
    }
}
#endif