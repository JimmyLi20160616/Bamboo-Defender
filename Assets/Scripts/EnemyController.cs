using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movespeed = 1;
    public int health = 10;

    private Rigidbody2D rb;
    private Vector2 moveDirection = Vector2.down;
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       // rb.transform.Translate((Vector3)moveDirection * movespeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    { /*
        if (other.collider.CompareTag("Wall"))       // stupid movement for enemy, replace here by the navigation
        {
            if (moveDirection == Vector2.down)
            {
                moveDirection = Vector2.right;
            }
            else if (moveDirection == Vector2.right)
            {
                moveDirection = Vector2.up;
            }
            else if (moveDirection == Vector2.up)
            {
                moveDirection = Vector2.left;
            }
            else if (moveDirection == Vector2.left)
            {
                moveDirection = Vector2.down;
            }
        }
        */
        if (other.collider.CompareTag("Enemy"))
        // hard to control which one will take damage first, so I send both of them to levelManager and get infos back at same time
        {
            if (rb.GetComponent<SpriteRenderer>().color != other.collider.GetComponent<SpriteRenderer>().color)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().GetResultOfEnemyFight(this.gameObject);
            }
        }

        else if (other.collider.CompareTag("Player"))   // Player will be sent back home when collides enemy.
        {
            other.collider.GetComponent<PlayerController>().FlashBackBase();
        }
    }

    public void TakeDamage(int h)
    {
        health -= h;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))   // lose when enemy reach the base
        {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().Lose();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
