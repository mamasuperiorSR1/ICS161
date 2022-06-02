using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetController : MonoBehaviour
{
    //[SerializeField] CameraController mainCamera;

    [SerializeField] public float engineThrust = 10000f;
    [SerializeField] public float pitchSpeed = 30f;
    [SerializeField] public float rollSpeed = 45f;
    [SerializeField] public float yawSpeed = 25f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;

    [SerializeField] private float thrust;
    [SerializeField] private float pitch;
    [SerializeField] private float roll;
    [SerializeField] private float yaw;

    private bool LandingGearDeployed = true;
    [SerializeField] private GameObject frontLandingGear;
    [SerializeField] private GameObject rearLandingGear;
    [SerializeField] private float GearSpeed;

    internal float speed;
    internal float height;
    internal float throttle { get { return thrust; } }

    private const float mToKm = 3.6f;
    private const float kmToKnots = 0.5399f;
    private const float aerodynamicEffect = 0.1f;

    private void Start()
    {
        anim.SetFloat("Left Rudder Left", 0f);
        anim.SetFloat("Left Rudder Right", 0f);
        anim.SetFloat("Right Rudder Left", 0f);
        anim.SetFloat("Right Rudder Right", 0f);
        anim.SetFloat("Left Elevator Up", 0f);
        anim.SetFloat("Left Elevator Down", 0f);
        anim.SetFloat("Right Elevator Up", 0f);
        anim.SetFloat("Right Elevator Down", 0f);
        anim.SetFloat("Front Landing Gear Up", 0f);
        anim.SetFloat("Front Landing Gear Down", 0f);
        anim.SetFloat("Rear Landing Gear Up", 0f);
        anim.SetFloat("Rear Landing Gear Down", 0f);
    }

    private void Update()
    {
        //Clear old values
        pitch = 0f;
        roll = 0f;
        yaw = 0f;

        if (height > 100f)
        {
            anim.SetFloat("Front Landing Gear Up", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Rear Landing Gear Up", 1f, 0.1f, Time.deltaTime);
        }

        if (height < 100f)
        {
            anim.SetFloat("Front Landing Gear Down", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Rear Landing Gear Down", 1f, 0.1f, Time.deltaTime);
        }

        //Update control inputs
        if (Input.GetKey(KeyCode.Q))
        { 
            yaw = -1f;
            anim.SetFloat("Left Rudder Left", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Rudder Left", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            anim.SetFloat("Left Rudder Left", 0f);
            anim.SetFloat("Right Rudder Left", 0f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            yaw = 1f;
            anim.SetFloat("Left Rudder Right", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Rudder Right", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetFloat("Left Rudder Right", 0f);
            anim.SetFloat("Right Rudder Right", 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            roll = 1f;
            anim.SetFloat("Left Elevator Up", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Elevator Down", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            roll = 1f;
            anim.SetFloat("Left Elevator Up", 0f);
            anim.SetFloat("Right Elevator Down", 0f);
        }


        if (Input.GetKey(KeyCode.D))
        {
            roll = -1f;
            anim.SetFloat("Left Elevator Down", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Elevator Up", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            roll = -1f;
            anim.SetFloat("Left Elevator Down", 0f);
            anim.SetFloat("Right Elevator Up", 0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            pitch = 1f;
            anim.SetFloat("Left Elevator Down", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Elevator Down", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            pitch = 1f;
            anim.SetFloat("Left Elevator Down", 0f);
            anim.SetFloat("Right Elevator Down", 0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pitch = -1f;
            anim.SetFloat("Left Elevator Up", 1f, 0.1f, Time.deltaTime);
            anim.SetFloat("Right Elevator Up", 1f, 0.1f, Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pitch = -1f;
            anim.SetFloat("Left Elevator Up", 0f);
            anim.SetFloat("Right Elevator Up", 0f);
        }

        UpdateThrottle();

        //Update height
        height = transform.position.y -1f;
    }

    private void UpdateThrottle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            thrust = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            thrust = 30f; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            thrust = 60f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            thrust = 100f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            thrust += 10f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            thrust -= 10f;
        }

        thrust = Mathf.Clamp(thrust, 0f, 100f);    
    }

    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, yaw * Time.fixedDeltaTime * yawSpeed);     //Yaw

        if (height > 20f)
        {
            transform.RotateAround(transform.position, transform.forward, roll * Time.fixedDeltaTime * rollSpeed); //Roll
        }

        if (rb.velocity.magnitude > 300f)
        {
            transform.RotateAround(transform.position, transform.right, pitch * Time.fixedDeltaTime * pitchSpeed); //Pitch
        }

        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        var localSpeed = Mathf.Max(0, localVelocity.z);
        speed = (localSpeed * mToKm) * kmToKnots;

        var aerofactor = Vector3.Dot(transform.forward, rb.velocity.normalized);
        aerofactor *= aerofactor;
        rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * localSpeed, aerofactor * localSpeed * aerodynamicEffect * Time.fixedDeltaTime);

        rb.AddForce((thrust * engineThrust) * transform.forward);
    }
}
