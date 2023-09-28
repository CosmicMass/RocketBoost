using UnityEngine;
using UnityEngine.UI;

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

    public float maxFuel = 100f;
    public float currentFuel;
    public Image FuelFill;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentFuel = maxFuel;
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
        if (currentFuel > 0)
        {
            // Deduct fuel based on thrust
            currentFuel -= mainThrust * Time.fixedDeltaTime;

            // Update the fuel bar UI
            UpdateFuelBar();

            // Apply thrust
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
        else
        {
            // Out of fuel, handle as needed (e.g., stop thrusting or trigger a game over).
        }
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

    public void UpdateFuelBar()
    {
        // Calculate the fill percentage based on currentFuel and maxFuel
        float fuelPercentage = currentFuel / maxFuel;

        // Update your UI element to reflect the fuelPercentage
        // You might need to adjust the fill amount or scale of the UI element.
        // For example, if you're using a Slider, you can set its value:
        FuelFill.fillAmount = fuelPercentage;
    }
}
