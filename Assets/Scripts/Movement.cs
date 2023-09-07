
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    public Joystick joystick;

    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip engineThrust;

    [SerializeField] ParticleSystem mainEngingeParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;
    public bool isThrusting = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (isThrusting)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        ProcessRotation();
    }


    public void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThrust);
        }
        if (!mainEngingeParticles.isPlaying)
        {
            mainEngingeParticles.Play();
        }
        isThrusting = true;
    }

    public void StopThrusting()
    {
        audioSource.Stop();
        mainEngingeParticles.Stop();
        isThrusting = false;
    }

    void ProcessRotation()
    {

        float rotationInput = joystick.Horizontal; // Use joystick's horizontal input for rotation

        // Rotate left when joystick is moved left
        if (rotationInput < -0.2f)
        {
            RotateLeft();
        }
        // Rotate right when joystick is moved right
        else if (rotationInput > 0.2f)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
    
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system take over
    }
}
