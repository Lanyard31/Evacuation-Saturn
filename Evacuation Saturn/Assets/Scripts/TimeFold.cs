using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeFold : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] private double JumpAmount;
    private double JumpTime;
    public bool FoldOnline = false;

    private void Awake()
    {
        playableDirector.playOnAwake = true;
        //playableDirector.Play();
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetButtonDown("Fire1"))
        {
            TimeJump();
        }
    }

    public void TimeJump()
    {
        this.GetComponent<AudioSource>().Play();
        JumpTime = playableDirector.time - JumpAmount;
        if (JumpTime <= 0)
        {
            JumpTime = 0;
        }
        playableDirector.time = JumpTime;
    }
}
