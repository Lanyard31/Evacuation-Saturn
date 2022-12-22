using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LaserSound : MonoBehaviour

{
    private ParticleSystem  _parentParticleSystem;

    private int _currentNumberOfParticles = 0;

    [SerializeField] public AudioClip[] BornSounds;
    [SerializeField] public AudioClip[] DieSounds;


    void Start()
    {
        _parentParticleSystem = this.GetComponent<ParticleSystem>();
        if(_parentParticleSystem == null)
            Debug.LogError("Missing ParticleSystem!", this);


        //EazySoundManager.IgnoreDuplicateSounds = false;

        //_explosionPhysicsForceEffect = this.GetComponent<ExplosionPhysicsForceEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        var amount = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);

        if (_parentParticleSystem.particleCount < _currentNumberOfParticles) 
        { 
            StartCoroutine(PlaySound(DieSounds[Random.Range(0, DieSounds.Length)], amount));
        } 

        if (_parentParticleSystem.particleCount > _currentNumberOfParticles) 
        { 
            StartCoroutine(PlaySound(BornSounds[Random.Range(0, BornSounds.Length)], amount));
        } 

        _currentNumberOfParticles = _parentParticleSystem.particleCount;
    }


    private IEnumerator PlaySound(AudioClip sound, int amount)
    {

        yield return new WaitForSeconds(0.1f);
    }

    private void ApplyExplosionForceInArea()
    {
        //if (_explosionPhysicsForceEffect != null)
        //    _explosionPhysicsForceEffect.ApplyExplosionForce();
    }

}
