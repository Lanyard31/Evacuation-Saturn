using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ships moves up and down")]
    [SerializeField] float controlSpeed = 10f;
    private float controlSpeedStored;
    private float controlSpeedBoosted;
    private float controlRollFactorBoosted;

    [Tooltip("How far player ship moves horizontally")][SerializeField] public float xRange = 10f;
    [Tooltip("How far player ship moves vertically")][SerializeField] public float yRange = 7f;

    [Header("Laser Array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen Position based tuning")]

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;
    private float controlRollFactorStored;

    public float pitchMin;
    public float pitchMax;
    public float volumeMin;
    public float volumeMax;
    [SerializeField] float evasiveBoost = 1.5f;

    [Header("Laser SFX")]
    [SerializeField] AudioSource laserSFX;
    [SerializeField] AudioClip laserClip;
    [SerializeField] bool laserTimer = true;
    [SerializeField] float laserTimerDuration = 0.5f;
    float roll;


    float xThrow, yThrow;
    bool evasive = false;
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    int lastXThrow = -1;
    public bool crippled = false;
    public bool finaleDisabled = false;

    [SerializeField] CollisionHandler CH;


    void Start() {
            evasive = false;
            controlSpeedStored = controlSpeed;
            controlRollFactorStored = controlRollFactor;

            controlSpeedBoosted = controlSpeed * evasiveBoost;
            controlRollFactorBoosted = controlRollFactor * evasiveBoost * 2;
    }

    void Update()
    {
        if (CH.tumbling == false)
        {
            ProcessEvasive();
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }



    void ProcessEvasive()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || (Input.GetAxis("XBOX_LT") >= 0.1f))
        {
            evasive = true;
            controlSpeed = controlSpeedBoosted;
            controlRollFactor = controlRollFactorBoosted;
        }
        else
        {
            evasive = false;
            controlSpeed = controlSpeedStored;
            controlRollFactor = controlRollFactorStored;
        }
    }

    void ProcessRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;

        // float rollLastFrame = roll;
        // roll = Mathf.Lerp(rollLastFrame, xThrow * controlRollFactor, t);
        // t += 0.2f * Time.deltaTime;

        if (evasive == false)
        {
            roll = xThrow * controlRollFactor;
            transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
        else
        {
            //roll = xThrow * controlRollFactor;
            if (xThrow > 0)
            {
                roll = roll - 8 * Time.deltaTime * 50;
                lastXThrow = -1;
            }
            else if (xThrow < 0)
            {
                roll = roll + 8 * Time.deltaTime * 50 ;
                lastXThrow = 1;
            }
            else
            {
                roll = roll + (8 * lastXThrow) * Time.deltaTime * 50;
            }
            transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        }
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        if (crippled == true)
        {
            xThrow = xThrow * 0.8f;
            yThrow = yThrow * 0.8f;
        }

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (finaleDisabled == false)
        {
            if (Input.GetKey(KeyCode.Space) == true || (Input.GetAxis("XBOX_RT") >= 0.1f))
            {
                SetLasersActive(true);
            }
            else
            {
                SetLasersActive(false);
            }
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
            if (isActive && laserTimer)
                {
                laserSFX.pitch = Random.Range(pitchMin, pitchMax);
                laserSFX.volume = Random.Range(volumeMin, volumeMax);
                laserSFX.PlayOneShot(laserClip);
                laserTimer = false;
                Invoke("LaserTimerReset", laserTimerDuration);
                }

        }
    }

    void LaserTimerReset()
    {
        laserTimer = true;
    }
}
