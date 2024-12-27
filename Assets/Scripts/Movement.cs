using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrenght = 100f;
    [SerializeField] float rotationStrenght = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThurstParticle;
    [SerializeField] ParticleSystem rightThurstParticle;

    Rigidbody rb;
    AudioSource audioSource;
    
    void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        PrecessThrust();
        PrecessRotation();
    }

    private void PrecessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrenght * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }
    
    void PrecessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RightRotate();
        }
        else if (rotationInput > 0)
        {
            LeftRotate();
        }
        else
        {
            StopRotating();
        }
    }
    private void RightRotate()
    {
        ApplyRatation(rotationStrenght);
        if (!rightThurstParticle.isPlaying)
        {
            leftThurstParticle.Stop();
            rightThurstParticle.Play();
        }
    }
    private void LeftRotate()
    {
        ApplyRatation(-rotationStrenght);
        if (!leftThurstParticle.isPlaying)
        {
            rightThurstParticle.Stop();
            leftThurstParticle.Play();
        }
    }
    private void StopRotating()
    {
        rightThurstParticle.Stop();
        leftThurstParticle.Stop();
    }

    private void ApplyRatation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

}
