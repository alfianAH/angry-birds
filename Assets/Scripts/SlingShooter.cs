using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D collider;
    public LineRenderer trajectory;
    private Vector2 startPos;

    [SerializeField] private float radius = 0.75f;

    [SerializeField] private float throwSpeed = 30f;

    private Bird bird;
    
    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseUp()
    {
        collider.enabled = false;
        Vector2 velocity = startPos - (Vector2) transform.position;
        float distance = Vector2.Distance(startPos, transform.position);
        
        bird.Shoot(velocity, distance, throwSpeed);
        
        // Set sling shooter to start position
        gameObject.transform.position = startPos;

        trajectory.enabled = false;
    }

    private void OnMouseDrag()
    {
        // Change mouse position to world position
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate it so that the 'rubber' of the catapult is within the specified radius
        Vector2 dir = p - startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;
        transform.position = startPos + dir;

        float distance = Vector2.Distance(startPos, transform.position);
        
        if (!trajectory.enabled) trajectory.enabled = true;
        DisplayTrajectory(distance);
    }

    private void DisplayTrajectory(float distance)
    {
        if(bird == null) return;

        Vector2 velocity = startPos - (Vector2) transform.position;
        int segmentCount = 5;
        Vector2[] segments = new Vector2[segmentCount];
        
        // Trajectory's start position is mouse's current position
        segments[0] = transform.position;
        
        // Start velocity
        Vector2 segVelocity = velocity * throwSpeed * distance;
        for (int i = 1; i < segmentCount; i++)
        {
            float elapsedTime = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * elapsedTime +
                          0.5f * Physics2D.gravity * Mathf.Pow(elapsedTime, 2);
        }
        
        trajectory.positionCount = segmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            trajectory.SetPosition(i, segments[i]);
        }
    }

    public void InitiateBird(Bird bird)
    {
        this.bird = bird;
        this.bird.MoveTo(gameObject.transform.position, gameObject);
        collider.enabled = true;
    }
}
