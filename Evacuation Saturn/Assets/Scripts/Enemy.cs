using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int HP = 2;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
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
        //if (other.tag != "Enemy")
        //{
            HP -= 1;
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parentGameObject.transform;
        //}
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(1);
    }
    
    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }


}
