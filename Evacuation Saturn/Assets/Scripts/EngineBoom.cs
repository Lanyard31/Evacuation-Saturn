using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBoom : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    public void Boomer()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity); //parentGameObject.transform);
        Debug.Log("VFX Created");
    }
}
