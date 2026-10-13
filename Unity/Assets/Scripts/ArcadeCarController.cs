using UnityEngine;

// Attach to the car root GameObject. Add a Rigidbody and a Collider to the car.
[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour
{
    [Header("Driving")]
    public float acceleration = 22f;
    public float reverseAcceleration = 10f;
    public float maxSpeed = 28f;
    public float steeringStrength = 95f;
    public float brakeStrength = 30f;
    public float grip = 5f;

    private Rigidbody body;
    private float throttle;
    private float steering;
    private bool braking;

    public float SpeedKmh => body == null ? 0f : body.velocity.magnitude * 3.6f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.mass = 1200f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        body.centerOfMass = new Vector3(0f, -0.45f, 0f);
    }

    private void Update()
    {
        // Keyboard controls for the first prototype. Mobile buttons can call SetControls().
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");
        braking = Input.GetKey(KeyCode.Space);
    }

    public void SetControls(float drive, float turn, bool brake)
    {
        throttle = Mathf.Clamp(drive, -1f, 1f);
        steering = Mathf.Clamp(turn, -1f, 1f);
        braking = brake;
    }

    private void FixedUpdate()
    {
        Vector3 forwardVelocity = Vector3.Project(body.velocity, transform.forward);
        Vector3 sidewaysVelocity = body.velocity - forwardVelocity;

        if (throttle > 0f && body.velocity.magnitude < maxSpeed)
            body.AddForce(transform.forward * throttle * acceleration, ForceMode.Acceleration);
        else if (throttle < 0f)
            body.AddForce(transform.forward * throttle * reverseAcceleration, ForceMode.Acceleration);

        if (braking)
            body.AddForce(-body.velocity.normalized * brakeStrength, ForceMode.Acceleration);

        float speedFactor = Mathf.Clamp01(body.velocity.magnitude / 3f);
        if (body.velocity.sqrMagnitude > 0.2f)
        {
            float direction = Vector3.Dot(body.velocity, transform.forward) >= 0f ? 1f : -1f;
            transform.Rotate(0f, steering * steeringStrength * speedFactor * direction * Time.fixedDeltaTime, 0f);
        }

        body.AddForce(-sidewaysVelocity * grip, ForceMode.Acceleration);

        if (body.velocity.magnitude > maxSpeed)
            body.velocity = body.velocity.normalized * maxSpeed;
    }
}