using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refugee : MonoBehaviour
{
    [SerializeField] GameObject refugeeDeathVFX;
    [SerializeField] GameObject refugeeHitVFX;
    [SerializeField] GameObject deathRadio;
    [SerializeField] int HP = 50;

    GameObject parentGameObject;

    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void Update()
    {
        if (HP <= 0)
        {
            KillRefugee();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        HitEffect();
    }

    void OnTriggerEnter(Collider other)
    {
        HitEffect();
    }

    public void KillRefugee()
    {
        GameObject vfx = Instantiate(refugeeDeathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        GameObject vfx2 = Instantiate(deathRadio, transform.position, Quaternion.identity);
        vfx2.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    void HitEffect()
    {
        HP--;
        GameObject vfx = Instantiate(refugeeHitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }
}
