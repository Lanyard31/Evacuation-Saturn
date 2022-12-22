using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int HP = 5;

    GameObject parentGameObject;

    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void Update()
    {
        if (HP <= 0)
        {
            KillEnemy();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        HP -= 1;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

}
