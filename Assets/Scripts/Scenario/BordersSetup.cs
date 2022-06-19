using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordersSetup : MonoBehaviour
{
    private Camera cam;
    private EdgeCollider2D edges;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        edges = GetComponent<EdgeCollider2D>();

        List<Vector2> newEdgePoints = new List<Vector2>();
        newEdgePoints.Add(cam.ScreenToWorldPoint(new Vector2(0, 0)));
        newEdgePoints.Add(cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, 0)));
        newEdgePoints.Add(cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight)));
        newEdgePoints.Add(cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight)));
        newEdgePoints.Add(cam.ScreenToWorldPoint(new Vector2(0, 0)));
        edges.SetPoints(newEdgePoints);

    }

}
