using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController trailController;
    public List<Bird> birds;
    public List<Enemy> enemies;

    private bool isGameEnded = false;

    private void Start()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].onBirdDestroyed += ChangeBird;
            birds[i].onBirdShot += AssignTrail;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].onEnemyDestroyed += CheckGameEnd;
        }
        slingShooter.InitiateBird(birds[0]);
    }

    public void AssignTrail(Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
    }

    public void ChangeBird()
    {
        if (isGameEnded) return;
        
        birds.RemoveAt(0);
        
        if (birds.Count > 0)
        {
            slingShooter.InitiateBird(birds[0]);
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
        }
    }
}
