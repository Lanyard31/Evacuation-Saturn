using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 6f;
    Enemy enemyScript;

    void Start()
    {
        timeTillDestruction = Random.Range(6f, 8f);
        enemyScript = GetComponent<Enemy>();
        Invoke("Boom", timeTillDestruction);
    }

    void Boom()
    {
        if (enemyScript != null)
        {
            enemyScript.KillEnemy();
        }

        Destroy(gameObject);
    }

}
