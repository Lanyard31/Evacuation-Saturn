using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySelf : MonoBehaviour
{
    [SerializeField] ParticleSystem Explosion;
    [SerializeField] AudioSource EnginePop;
    // Start is called before the first frame update
    void Start()
    {
        Explosion.Play();
        EnginePop.Play();
    }


}
