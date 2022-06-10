using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBullet : Poolable
{

    public float speed = 10f;
    public float initialLifeTime = 2f;
    private float remainingLifeTime;
    public Vector2 direction = new Vector2(1, 1);

    // TODO: Add damage
    //public float damage = 1f;


    private Camera cam;
    private Rigidbody2D rb;
    private Collider2D col;
    private TrailRenderer trail;
    private SpriteRenderer sprite;

    private void OnEnable()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Shoot(Vector2 initialDirection)
    {
        direction = initialDirection.normalized * speed;
        col.isTrigger = true;
    }
    public void Shoot(Vector2 initialPosition, Vector2 initialDirection)
    {
        transform.position = initialPosition;
        Shoot(initialDirection);
    }

    public override Poolable Spawn()
    {
        trail.Clear();
        gameObject.SetActive(true);
        remainingLifeTime = initialLifeTime;
        onSpawn.Invoke();
        return this;
    }
    public override Poolable Despawn()
    {
        trail.Clear();
        gameObject.SetActive(false);
        onDespawn.Invoke();
        return this;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0)
        {
            Despawn();
            return;
        }
        WrapAroundBorders();
    }

    private void WrapAroundBorders()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);
        // Teleport to the other side of the screen if the object is outside
        if (screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector2(screenPosition.x > Screen.width ? 0 : Screen.width, screenPosition.y));
            trail.Clear();
        }
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector2(screenPosition.x, screenPosition.y > Screen.height ? 0 : Screen.height));
            trail.Clear();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<ShipMovement>().Push(direction * 20);
            Despawn();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            col.isTrigger = false;
        }
    }

}