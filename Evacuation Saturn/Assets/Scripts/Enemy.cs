using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int HP = 2;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void Update()
    {
        if (HP <= 0)
        {
            ProcessHit();
            KillEnemy();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        HP -= 1;
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(1);
    }
    
    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }


}
