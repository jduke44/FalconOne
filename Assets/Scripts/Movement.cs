using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - Things that can be tuned in the editor.

    // CACHE - e.g. References for readability or speed of loading.

    // STATE - private instances (member) variables.

    [SerializeField] float mainThruster = 100f;
    [SerializeField] float rotateThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    Rigidbody rb;
    AudioSource m_audioSource;

    //bool isThrustOn = false;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


        void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
            StopThrusting();

    }



    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThruster * Time.deltaTime); //Vector3.up is shorthand for (0,1,0) coordinates.
        if (!m_audioSource.isPlaying)
        {
            m_audioSource.PlayOneShot(mainEngine);
            //mainThrustParticles.Play();
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }  
    private void StopThrusting()
    {
        m_audioSource.Stop();
        mainThrustParticles.Stop();
    }
    



    void ProcessRotation()
    {
        ProcessRotating();
    }

    private void ProcessRotating()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Rotate Left");
            ApplyRotation(rotateThrust);
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log(" Rotate Right");
            ApplyRotation(-rotateThrust);
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
            else
            {
                rightThrustParticles.Stop();
                leftThrustParticles.Stop();
            }

        }
    }

    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation so we can manually collide with objects and still maintain control.
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation so the global physics system can take over again.
    }
}
