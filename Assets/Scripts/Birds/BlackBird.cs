using UnityEngine;

public class BlackBird : Bird
{
    [SerializeField] private float explosionForce = 50f, explosionRadius = 5f;
    [SerializeField] private bool hasExplode = false;
    
    public override void OnTap()
    {
        Explode();
    }

    private void Explode()
    {
        if((State == BirdState.HitSomething || State == BirdState.Thrown) && !hasExplode)
        {
            Debug.Log("Boom");
            Vector3 explosionPosition = transform.position;
            AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            hasExplode = true;
        }
    }

    private void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = BirdRigidbody.transform.position = explosionPosition;
        float wearoff = 1 - dir.magnitude / explosionRadius;
        BirdRigidbody.AddForce(dir.normalized * explosionForce * wearoff);
    }
}
