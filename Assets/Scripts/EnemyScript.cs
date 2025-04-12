using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float stuckDetectionTime = 2f;

    private int currentWaypointIndex = 0;
    private Transform player;
    private Rigidbody2D rb;
    private bool chasingPlayer = false;
    private float stuckTimer = 0f;
    private Vector2 lastPos;

    public GameObject healthBar;
    public float health = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPos = rb.position;
        healthBar.transform.localScale = new Vector3(1, 0.1375f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Chase player if player is within detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            chasingPlayer = true;
        }
        else
        {
            chasingPlayer = false;
        }

        // Scale healthbar based on heath
        healthBar.transform.localScale = new Vector3(health * 0.01f, 0.1375f, 1);
    
        // Destroy enemy game object if health drops to or below 0
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If colliding with player, remove a heart from the player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentHearts--;
            collision.gameObject.GetComponent<PlayerController>().Respawn();
        }

        // If colliding with the players weapon, remove hp and add knockback force
        if (collision.gameObject.tag == "Weapon")
        {
            health -= 25;
            Vector2 weaponPos = collision.transform.position;

            // TODO: FIX ADD FORCE
            //rb.AddForce(new Vector2(weaponPos.x - transform.position.x, weaponPos.y - transform.position.y ) * 50f);
        }
    }

    private void FixedUpdate()
    {
        // If chasing the player, move towards the players position
        // Otherwise, patrol between given waypoints
        if (chasingPlayer)
        {
            MoveTowards(player.position);
        }
        else
        {
            Patrol();
        }

        // Check if stuck
        StuckCheck();
    }

    // Move enemy towars a given direction
    void MoveTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    void Patrol()
    {
        // Avoid error via idling if no waypoints exist
        if (waypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        MoveTowards(targetWaypoint.position);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            stuckTimer = 0f;
        }
    }

    void StuckCheck()
    {
        if (Vector2.Distance(rb.position, lastPos) < 0.01f)
        {
            stuckTimer += Time.fixedDeltaTime;
        }
        else
        {
            stuckTimer = 0f;
        }

        if (stuckTimer >= stuckDetectionTime)
        {
            TurnAround();
            stuckTimer = 0f;
        }
        lastPos = rb.position;
    }

    void TurnAround()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
