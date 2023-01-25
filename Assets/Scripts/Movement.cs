using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private Rigidbody roketRigidbody;
    private AudioSource audioSource;
    public ParticleSystem thrustParticles;
    public ParticleSystem A_thrustParticles;
    public ParticleSystem D_thrustParticles;

    public AudioClip mainEngine;
    public float mainThrust = 100f;
    public float rotateThrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        roketRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ThustMethod();
        }
        else StopMainThrustExtras();

    }
    void StopMainThrustExtras() {
        if (audioSource.isPlaying)
            audioSource.Stop();
        if (thrustParticles.isPlaying)
            thrustParticles.Stop();
    }
    void ThustMethod() {
        if (!thrustParticles.isPlaying)
            thrustParticles.Play();

        roketRigidbody.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            RotateByKey(true);
        else if (Input.GetKey(KeyCode.D))
            RotateByKey(false);
        else
            StopRotatingProcess();
    }
    void StopRotatingProcess()
    {
        if (A_thrustParticles.isPlaying)
            A_thrustParticles.Stop();
        if (D_thrustParticles.isPlaying)
            D_thrustParticles.Stop();
    }
    void RotateByKey(bool isAKey) 
    {
            Rotate(isAKey?Vector3.forward:Vector3.back);
            if (!isAKey && !D_thrustParticles.isPlaying)
                D_thrustParticles.Play();
            if (isAKey && !A_thrustParticles.isPlaying)
                A_thrustParticles.Play();
    }
    void Rotate(Vector3 direction)
        {
            roketRigidbody.freezeRotation = true;  // freezing rotation from hitting
            transform.Rotate(rotateThrust * Time.deltaTime * direction);
            roketRigidbody.freezeRotation = false;  // un-freezing rotation from hitting
            
        }
}
