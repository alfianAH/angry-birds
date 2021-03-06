﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public  enum BirdState{ Idle, Thrown, HitSomething}
    private Rigidbody2D birdRigidbody;
    private CircleCollider2D birdCollider;
    
    public UnityAction onBirdDestroyed = delegate {  };
    public UnityAction<Bird> onBirdShot = delegate {  };
    
    private BirdState birdState;
    private float minVelocity = 0.05f;
    private bool flagDestroy = false;

    public BirdState State => birdState;
    protected Rigidbody2D BirdRigidbody => birdRigidbody;

    private void Start()
    {
        birdCollider = GetComponent<CircleCollider2D>();
        birdRigidbody = GetComponent<Rigidbody2D>();
        
        birdRigidbody.bodyType = RigidbodyType2D.Kinematic;
        birdCollider.enabled = false;
        birdState = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        if (birdState == BirdState.Idle &&
                        birdRigidbody.velocity.sqrMagnitude >= minVelocity)
        {
            birdState = BirdState.Thrown;
        }

        if ((birdState == BirdState.Thrown ||
            birdState == BirdState.HitSomething) &&
            birdRigidbody.velocity.sqrMagnitude < minVelocity &&
            !flagDestroy)
        {
            // Destroy object after 2 seconds
            // if its speed is less than minimum velocity
            flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        birdState = BirdState.HitSomething;
    }

    private void OnDestroy()
    {
        if(birdState == BirdState.Thrown || birdState == BirdState.HitSomething)
            onBirdDestroyed();
    }

    public virtual void OnTap()
    {
        // Do nothing
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Initiate position and change bird's parent
    /// </summary>
    /// <param name="target"></param>
    /// <param name="parent"></param>
    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }
    
    /// <summary>
    /// Throw bird to direction, distance and velocity
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="distance"></param>
    /// <param name="speed"></param>
    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        birdCollider.enabled = true;
        birdRigidbody.bodyType = RigidbodyType2D.Dynamic;
        birdRigidbody.velocity = velocity * speed * distance;
        onBirdShot(this);
    }
}
