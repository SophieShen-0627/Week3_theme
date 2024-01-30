using UnityEngine;

public class FishFlock : MonoBehaviour
{
    [SerializeField] Vector3 OriginPlc;

    PlayerMovement player;

    private void Start()
    {
        OriginPlc = (Vector3)transform.position;
        player = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        ApplyFlockingRules();
        FindPlayer();
        Move();
    }

    void FindPlayer()
    {
        Vector2 playerPos = Vector2.zero;

        if (Vector2.Distance(player.transform.position, OriginPlc) <= FishFlockManager.instance.ChasingPlayerDistance )
        {
            playerPos = player.transform.position;
        }
        else
        {
            playerPos = Vector2.zero;
        }


        if (playerPos != Vector2.zero)
            transform.position += ((Vector3)playerPos - transform.position) * Time.deltaTime * FishFlockManager.instance.ChasingPlayerSpeed;
    }
    void ApplyFlockingRules()
    {
        Vector2 cohesion = Vector2.zero;
        Vector2 separation = Vector2.zero;
        Vector2 alignment = Vector2.zero;


        int neighborsCount = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, FishFlockManager.instance.neighborRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider != this.GetComponent<Collider2D>() && collider.GetComponent<FishFlock>())
            {
                cohesion += (Vector2)collider.transform.position;
                alignment += (Vector2)collider.GetComponent<Rigidbody2D>().velocity;

                if (Vector2.Distance(transform.position, collider.transform.position) < FishFlockManager.instance.separationRadius)
                {
                    separation += (Vector2)(transform.position - collider.transform.position);
                }

                neighborsCount++;
            }

        }

        if (neighborsCount > 0)
        {
            cohesion /= neighborsCount;
            alignment /= neighborsCount;

            cohesion = (cohesion - (Vector2)transform.position).normalized;
            alignment = alignment.normalized;

            Vector2 targetDirection = (cohesion + alignment).normalized;

            // Adjust the rotation towards the target direction
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), FishFlockManager.instance.rotationSpeed * Time.deltaTime);

            // Apply separation force
            transform.position += (Vector3)separation.normalized * FishFlockManager.instance.speed * Time.deltaTime * FishFlockManager.instance.SeperationRate;


        }
    }

    void Move()
    {
        // Move forward
        transform.position += transform.right * FishFlockManager.instance.speed * Time.deltaTime;


    }
}