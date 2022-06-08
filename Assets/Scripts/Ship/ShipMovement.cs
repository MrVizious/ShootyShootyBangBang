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

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
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
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
