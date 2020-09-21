using UnityEngine;

public class BlackBird : Bird
{
    [SerializeField] private bool hasExplode = false;
    [SerializeField] private GameObject explosionParticle,
        explosion;
    
    public override void OnTap()
    {
        Explode();
    }

    private void Explode()
    {
        if((State == BirdState.HitSomething || State == BirdState.Thrown) && !hasExplode)
        {
            hasExplode = true;
            
            explosion.SetActive(true);
            GameObject explosionDuplicate = Instantiate(explosionParticle, transform.position, Quaternion.identity, transform.parent);
            ParticleSystem explosionParticleDuplicate = explosionDuplicate.GetComponent<ParticleSystem>();
            explosionParticleDuplicate.Play();
            
            Destroy(gameObject, explosionParticleDuplicate.main.duration);
        }
    }
}
