using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController trailController;
    public List<Bird> birds;
    public List<Enemy> enemies;
    public BoxCollider2D tapCollider;

    [SerializeField] private GameManager gameManager;

    private Bird shotBird;
    private bool isGameEnded;

    private void Start()
    {
        foreach (var bird in birds)
        {
            bird.onBirdDestroyed += ChangeBird;
            bird.onBirdShot += AssignTrail;
        }

        foreach (var enemy in enemies)
        {
            enemy.onEnemyDestroyed += CheckGameEnd;
        }
        
        tapCollider.enabled = false;
        slingShooter.InitiateBird(birds[0]);
        shotBird = birds[0];
    }

    private void OnMouseUp()
    {
        if (shotBird != null)
        {
            shotBird.OnTap();
        }
    }

    private void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    private void ChangeBird()
    {
        tapCollider.enabled = false;
        if (isGameEnded) return;
        
        birds.RemoveAt(0);
        try
        {
            shotBird = birds[0];
            if (birds.Count > 0)
            {
                slingShooter.InitiateBird(birds[0]);
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            gameManager.GameFailed();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if (enemies.Count == 0)
        {
            isGameEnded = true;
            gameManager.FinishGame();
        }
    }
}
