using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float movementAcceleration = 5.0f;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 direction = movement.normalized;
        Vector2 wishVel = direction * speed;

        velocity.x = Mathf.Lerp(rb.velocity.x, wishVel.x, movementAcceleration * Time.deltaTime);
        velocity.y = Mathf.Lerp(rb.velocity.y, wishVel.y, movementAcceleration * Time.deltaTime);


        rb.velocity = velocity;
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
