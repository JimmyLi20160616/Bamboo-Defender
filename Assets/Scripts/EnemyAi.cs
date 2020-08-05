using Pathfinding;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{

    public Transform target;
    public float speed = 2;

    public float nextWPdist = .5f;

    Path path;
    int currentWP = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Pathwork", 0f, .5f);
    }

    void Pathwork()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, PathComplete);
    }


    void PathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWP = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;
        if (currentWP >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
        Vector2 f = dir * speed * Time.deltaTime;

        rb.AddForce(f);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
        if ( distance < nextWPdist)
        {
            currentWP++;
        }
        
    }
}
