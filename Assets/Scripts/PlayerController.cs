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
    GameObject[] hearts;
    public int currentHearts;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHearts = maxHearts;
        hearts = new GameObject[maxHearts];

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = GameObject.Instantiate(HeartPrefab);
            hearts[i].gameObject.transform.position = new Vector3(hearts[i].gameObject.transform.position.x + i * 1.5f, hearts[i].gameObject.transform.position.y, hearts[i].gameObject.transform.position.z);
            hearts[i].gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleHealth();
    }

    void HandleHealth()
    {
        // If hearts reach 0, play fail scene
        if (currentHearts <= 0)
        {
            Death();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHearts)
            {
                if (hearts[i].gameObject.activeSelf != true)
                {
                    hearts[i].gameObject.SetActive(true);
                }
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }

    void HandleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 direction = movement.normalized;
        Vector2 wishVel = direction * speed;

        velocity.x = Mathf.Lerp(rb.velocity.x, wishVel.x, movementAcceleration * Time.deltaTime);
        velocity.y = Mathf.Lerp(rb.velocity.y, wishVel.y, movementAcceleration * Time.deltaTime);

        rb.velocity = velocity;
    }

    void Death()
    {
        SceneManager.LoadScene(2);
    }
}
