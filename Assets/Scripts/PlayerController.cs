using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    public GameObject wallHorizontal, wallVertical;
    public int numOfWall = 100; // test
    public float playerOffset = 0.34f;
    public float setWallTimeInterval = 2.0f;
    public float wallExistTime = 5.0f;

    private Rigidbody2D rb;
    private Vector2 faceDirection = Vector2.up;
    private float timer = 2.0f;
    private Vector3 originalPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = rb.transform.position;
    }
    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        SetWall();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            faceDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            faceDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            faceDirection = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            faceDirection = Vector2.right;
        }
    }

    void SetWall()
    {
        if (timer <= setWallTimeInterval)
        {
            timer += Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.Space) && numOfWall > 0 && timer >= setWallTimeInterval)
        {
            GameObject wall = faceDirection.x == 0 ? wallHorizontal : wallVertical;
            Vector3 playerPos = rb.transform.position;
            GameObject go = Instantiate(wall, playerPos + (Vector3)(faceDirection * playerOffset), Quaternion.identity);
            Destroy(go, wallExistTime);
            numOfWall--;
            timer = 0;
        }
        AstarPath.active.Scan();
    }

    public void FlashBackBase()
    {
        rb.transform.position = originalPos;
    }
}
