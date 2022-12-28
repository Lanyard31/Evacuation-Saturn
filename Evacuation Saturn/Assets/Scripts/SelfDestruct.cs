using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Boom", timeTillDestruction);
    }

    void Boom()
    {
        Destroy(gameObject);
    }

}
