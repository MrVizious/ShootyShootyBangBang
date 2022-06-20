using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundParallax : MonoBehaviour
{
    public Vector2 parallaxSpeed;
    public float rotationSpeed;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += parallaxSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
