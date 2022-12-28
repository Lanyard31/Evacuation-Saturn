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
            HitEffect();
        //}
    }

        void OnTriggerEnter(Collider other)
    {
        //if (other.tag != "Enemy")
        //{
            HitEffect();
        //}
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(1);
    }
    
    public void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    void HitEffect()
    {
        HP--;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }


}
