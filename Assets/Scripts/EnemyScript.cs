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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPos = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            chasingPlayer = true;
        }
        else
        {
            chasingPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currentHearts--;
        }
    }

    private void FixedUpdate()
    {
        if (chasingPlayer)
        {
            MoveTowards(player.position);
        }
        else
        {
            Patrol();
        }

        StuckCheck();
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    void Patrol()
    {
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
