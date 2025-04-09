using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10.0f;
    public float movementAcceleration = 5.0f;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 velocity;

    [Header("Health")]
    public int maxHearts;
    public GameObject HeartPrefab;
    public int currentHearts;
    GameObject[] hearts;

    [Header("Attack")]
    ATTACKDIR currentAttackDir;
    Vector3 currentAttackVector;
    public GameObject weaponSprite;
    bool isAttacking = false;
    bool canAttack = true;
    public float attackTimerMax = 0.5f;
    float attackTimer;

    [Header("Spawn")]
    public Transform playerSpawn;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = playerSpawn.position;

        // Set hearts to max
        currentHearts = maxHearts;
        hearts = new GameObject[maxHearts];

        // Instantiate Heart prefab and set parent to player object
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = GameObject.Instantiate(HeartPrefab);
            hearts[i].gameObject.transform.position = new Vector3(hearts[i].gameObject.transform.position.x + i * 1.5f, hearts[i].gameObject.transform.position.y, hearts[i].gameObject.transform.position.z);
            hearts[i].gameObject.transform.SetParent(this.gameObject.transform);
        }

        // Set weapon sprite to inactive
        weaponSprite.SetActive(false);

        // Set Timers to max
        attackTimer = attackTimerMax;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAttacks();
        HandleHealth();
    }

    void HandleHealth()
    {
        // If hearts reach 0, play fail scene
        if (currentHearts <= 0)
        {
            Death();
        }

        // Loop through heart prefabs
        for (int i = 0; i < hearts.Length; i++)
        {
            // Set heart prefab to active if current heart index is within the range of current hearts
            if (i < currentHearts)
            {
                if (hearts[i].gameObject.activeSelf != true)
                {
                    hearts[i].gameObject.SetActive(true);
                }
            }
            // Else, set heart to inactive
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    void HandleMovement()
    {
        // Get mouse input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set current attack direction
        if (movement.x < 0)
        {
            currentAttackDir = ATTACKDIR.LEFT;
            currentAttackVector = new Vector3(-1, 0, 0);
        }
        else if (movement.x > 0)
        {
            currentAttackDir = ATTACKDIR.RIGHT;
            currentAttackVector = new Vector3(1, 0, 0);
        }
        if (movement.y < 0)
        {
            currentAttackDir = ATTACKDIR.DOWN;
            currentAttackVector = new Vector3(0, -1, 0);
        }
        else if (movement.y > 0)
        {
            currentAttackDir = ATTACKDIR.UP;
            currentAttackVector = new Vector3(0, 1, 0);
        }

        // Normalize mouse input
        Vector2 direction = movement.normalized;

        // Create wish velocity based on normalized mouse movement and speed of player
        Vector2 wishVel = direction * speed;

        // Lerp between the players current velocity and the wish velocity by acceleration per second
        velocity.x = Mathf.Lerp(rb.velocity.x, wishVel.x, movementAcceleration * Time.deltaTime);
        velocity.y = Mathf.Lerp(rb.velocity.y, wishVel.y, movementAcceleration * Time.deltaTime);

        // Update player velocity
        rb.velocity = velocity;

    }

    void HandleAttacks()
    {
        // Check for attack input
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            isAttacking = true;
            canAttack = false;
        }

        // If player is attacking, reduce attackTimer and enable weapon
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            weaponSprite.transform.position = transform.position + currentAttackVector;
            weaponSprite.SetActive(true);
        }

        // Reset attack if attack timer reaches 0
        if (attackTimer <= 0)
        {
            isAttacking = false;
            canAttack = true;
            attackTimer = attackTimerMax;
        }

    }

    // If player dies, load fail scene
    void Death()
    {
        SceneManager.LoadScene(2);
    }
}


enum ATTACKDIR
{
    UP,
    DOWN,
    LEFT,
    RIGHT
};
