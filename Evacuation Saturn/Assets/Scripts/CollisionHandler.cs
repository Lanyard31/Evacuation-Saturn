using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem Explosion;
    [SerializeField] GameObject body;

    
    [SerializeField] int HP = 10;
    private int StartingHP;

    [SerializeField] GameObject[] LeftSide;
    [SerializeField] GameObject[] RightSide;
    [SerializeField] GameObject[] Body;

    private Rigidbody rb;
    public bool tumbling = false;

    Vector3 tumbleDirection;
    [SerializeField] float tumbleSpeed;
    [SerializeField] float controlsDisabledTime = 0.25f;
    PlayerControls controls;

    Vector3 rbStored;
    float zStored;
    public AudioSource playerboom;

    void Start()
    {
        StartingHP = HP;
        rb = GetComponent<Rigidbody>(); 
        controls = FindObjectOfType<PlayerControls>();
        rbStored = rb.velocity;

    }
    
    void Update()
    {
        Tumbler();
    }



    void OnTriggerEnter(Collider other)
    {
        if (tumbling == false)
        {
            HP--;
            //rbStored = rb.velocity;
            Debug.Log("HP: " + HP);
            zStored = transform.localPosition.z;
        }
        tumbling = true;
        //GetComponent<PlayerControls>().enabled = false;
        tumbleDirection = this.transform.position - other.transform.position;
        tumbleDirection = tumbleDirection.normalized;
        CheckHealth();
        Invoke("ReEnableControls", controlsDisabledTime);
     }

    void ReEnableControls()
    {
        GetComponent<PlayerControls>().enabled = true;
        //rb.velocity = rbStored;
        //transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, zStored);
        tumbling = false;
    }

    void Tumbler()
    {
        if (tumbling == false)
        {
            return;
        }
        else
        {
            Debug.Log(tumbleDirection);
            Debug.Log("old local " + transform.localPosition);
            transform.localPosition = new Vector3 (((transform.localPosition.x + tumbleDirection.x  + (tumbleSpeed * Time.deltaTime)/2)), (transform.localPosition.y + tumbleDirection.y + (tumbleSpeed * Time.deltaTime)), 0f);
            Debug.Log("new local " + transform.localPosition); 
            //rb.velocity = new Vector3 (tumbleDirection.x, tumbleDirection.y, 0) * tumbleSpeed * Time.deltaTime;


// transform.localPosition = new Vector3 ((transform.localPosition.x - (tumbleDirection.x * tumbleSpeed * Time.deltaTime)), (transform.localPosition.y + tumbleDirection.y + (tumbleSpeed * Time.deltaTime)), 0f);

            // float xOffset = tumbleDirection.x * Time.deltaTime * tumbleSpeed;
            // float rawXPos = transform.localPosition.x + xOffset;
            // float clampedXPos = Mathf.Clamp(rawXPos, (controls.xRange * -1), controls.xRange);

            // float yOffset = tumbleDirection.y * Time.deltaTime * tumbleSpeed;
            // float rawYPos = transform.localPosition.y + yOffset;
            // float clampedYPos = Mathf.Clamp(rawYPos, (controls.yRange * -1), controls.yRange);

            // transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        }

    }


    void CheckHealth()
    {


        if (HP < StartingHP * 0.5 && HP >= StartingHP * 0.25)
        {
            foreach (GameObject item in RightSide)
            {
                if (item != null)
                {
                    var emissionModule = item.GetComponent<ParticleSystem>().emission;
                    emissionModule.enabled = true;
                    item.GetComponent<PlayerHitFlash>().boom();
                }
            }
        }

        else if (HP < StartingHP * 0.25 && HP > 0)
        {
            foreach (GameObject item in LeftSide)
            {
                if (item != null)
                {
                    var emissionModule = item.GetComponent<ParticleSystem>().emission;
                    emissionModule.enabled = true;
                    item.GetComponent<PlayerHitFlash>().boom();
                }
            }
        }
        else if (HP <= 0)
        {
            StartCrashSequence();
        }

        foreach (GameObject item in Body)
        {
            if (item != null)
                {
                    item.GetComponent<PlayerHitFlash>().Flash();
                }
        }


    }

    void StartCrashSequence()
    {
        playerboom.Play();
        GetComponent<PlayerControls>().enabled = false;
        Explosion.Play();
        GetComponent<BoxCollider>().enabled = false;
        body.SetActive(false);
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
