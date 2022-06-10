using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [Range(0f, 100f)]
    public float maxSpeed = 10f;
    [Range(0f, 100f)]
    public float maxAcceleration = 10f;
    public Vector2 playerInput;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Camera cam;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        playerInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        playerInput = Vector2.ClampMagnitude(playerInput, 1);
        rb.AddForce(playerInput * maxAcceleration);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        KeepInCamera();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Force);
    }

    private void KeepInCamera()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);
        // Teleport to the other side of the screen if the object is outside
        if (screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector2(screenPosition.x > Screen.width ? Screen.width : 0, screenPosition.y));
        }
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector2(screenPosition.x, screenPosition.y > Screen.height ? Screen.height : 0));
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
