using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audiosrc;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainengine;
    [SerializeField] ParticleSystem rocketThrust;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        EngineThrust();

    }

    

    void ProcessRotation()
    {
        SideThrust();
    }

    

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;


    }
    void EngineThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }

            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audiosrc.isPlaying)
            {
                audiosrc.PlayOneShot(mainengine);
            }
        }
        else
        {
            rocketThrust.Stop();
            audiosrc.Stop();
        }
    }
    void SideThrust()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (!leftThrust.isPlaying)
            {
                leftThrust.Play();
            }
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!rightThrust.isPlaying)
            {
                rightThrust.Play();
            }

            ApplyRotation(-rotationThrust);
        }
        else
        {
            leftThrust.Stop();
            rightThrust.Stop();
        }
    }
}

