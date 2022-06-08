using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLookAt : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePosition - (Vector2)transform.position;
    }
}
