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

    private Rigidbody rb;
    bool tumbling = false;

    Vector3 tumbleDirection;
    [SerializeField] float tumbleSpeed;
    PlayerControls controls;

    Vector3 rbStored;
    float zStored;

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
            Debug.Log(HP);
            zStored = transform.localPosition.z;
        }
        tumbling = true;
        GetComponent<PlayerControls>().enabled = false;
        tumbleDirection = rb.transform.position - other.transform.position;
        tumbleDirection = tumbleDirection.normalized;
        Invoke("ReEnableControls", 0.2f);
     }

    void ReEnableControls()
    {
        GetComponent<PlayerControls>().enabled = true;
        rb.velocity = rbStored;
        transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, zStored);
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

            rb.velocity = new Vector3 (tumbleDirection.x, tumbleDirection.y, rb.velocity.z) * tumbleSpeed * Time.deltaTime;


            // float xOffset = tumbleDirection.x * Time.deltaTime * tumbleSpeed;
            // float rawXPos = transform.localPosition.x + xOffset;
            // float clampedXPos = Mathf.Clamp(rawXPos, (controls.xRange * -1), controls.xRange);

            // float yOffset = tumbleDirection.y * Time.deltaTime * tumbleSpeed;
            // float rawYPos = transform.localPosition.y + yOffset;
            // float clampedYPos = Mathf.Clamp(rawYPos, (controls.yRange * -1), controls.yRange);

            // transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        }

    }

    void FixedUpdate()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (HP < StartingHP * 1 && HP >= StartingHP * 0.6)
        {
            foreach (GameObject item in RightSide)
            {
                item.GetComponent<PlayerHitFlash>().Flash();
            }
            foreach (GameObject item in LeftSide)
            {
                item.GetComponent<PlayerHitFlash>().Flash();
            }
        }

        if (HP < StartingHP * 0.6 && HP >= StartingHP * 0.3)
        {
            foreach (GameObject item in RightSide)
            {
                if (item != null)
                {
                    item.GetComponent<PlayerHitFlash>().boom();
                }
            }
            foreach (GameObject item in LeftSide)
            {
                item.GetComponent<PlayerHitFlash>().Flash();
            }
        }

        else if (HP < StartingHP * 0.3 && HP > 0)
        {
            foreach (GameObject item in RightSide)
            {
                if (item != null)
                {
                    item.GetComponent<PlayerHitFlash>().boom();
                }
            }
            foreach (GameObject item in LeftSide)
            {
                if (item != null)
                {
                    item.GetComponent<PlayerHitFlash>().boom();
                }
            }
        }
        else if (HP <= 0)
        {
            StartCrashSequence();
        }


    }

    void StartCrashSequence()
    {
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
