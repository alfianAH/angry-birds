using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private GameObject obstacleDestroyParticle;
    private bool isHit = false;

    private void OnDestroy()
    {
        if(isHit)
        {
            GameObject ashParticle = Instantiate(obstacleDestroyParticle, transform.position, Quaternion.identity,
                transform.parent);
            ParticleSystem explosionParticleDuplicate = ashParticle.GetComponent<ParticleSystem>();
            explosionParticleDuplicate.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (other.gameObject.CompareTag("Bird"))
        {
            isHit = true;
            Destroy(gameObject);
        }
        else
        {
            float damage = other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            // Debug.Log("Damage: " + damage);
            health -= damage;
            // Debug.Log("Health: " + health);
            if (health <= 0)
            {
                isHit = true;
                Destroy(gameObject);
            }
        }
    }
}
