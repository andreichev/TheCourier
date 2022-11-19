using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarConroller : MonoBehaviour
{
    public Transform centerOfMass;

    [HideInInspector]
    public InputStr inputCar;
    public struct InputStr
    {
        public float Forward;
        public float Steer;

        public bool isBreaking()
        {
            return Forward < 0;
        }
    }

    public WheelInfo[] Wheels;

    public float maxSpeed = 15f;

    public float MotorPower = 5000f;
    public float SteerAngle = 35f;

    private Rigidbody rigidbody;
    private PlayerObserver playerObserver;
    private GameMenu gameMenu;

    [Range(0, 1)]
    public float KeepGrip = 1f;
    public float Grip = 5f;

    private float currentSpeed = 0f;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        playerObserver = this.GetComponent<PlayerObserver>();
        gameMenu = GameObject.FindObjectOfType<GameMenu>();

        rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        inputCar.Forward = Input.GetAxis("Vertical");
        inputCar.Steer = Input.GetAxis("Horizontal");

        currentSpeed = rigidbody.velocity.magnitude;
        if (currentSpeed > maxSpeed)
        {
            inputCar.Forward = 0;
        }

        for (int i = 0; i < Wheels.Length; i++)
        {
            if (Wheels[i].Motor)
                Wheels[i].WheelCollider.motorTorque = inputCar.Forward * MotorPower;

            if (Wheels[i].Steer)
                Wheels[i].WheelCollider.steerAngle = inputCar.Steer * SteerAngle;

            Wheels[i].Rotation += Wheels[i].WheelCollider.rpm / 60 * 360 * Time.fixedDeltaTime;
            Wheels[i].MeshRenderer.localRotation = Quaternion.Euler(Wheels[i].Rotation, 
                Wheels[i].isLeftWheel ? Wheels[i].WheelCollider.steerAngle - 180 : Wheels[i].WheelCollider.steerAngle,
                0);
        }

        //rigidbody.AddForceAtPosition(transform.up * rigidbody.velocity.magnitude * -0.1f * Grip, transform.position + transform.rotation * centerOfMass.localPosition);
    }

    void Update()
    {
        if (gameMenu != null)
        {
            gameMenu.setSpeed(currentSpeed * 3.6f);
        }
    }

    void OnValidate()
    {
        for (int i = 0; i < Wheels.Length; i++)
        {
            //settings
            var ffriction = Wheels[i].WheelCollider.forwardFriction;
            var sfriction = Wheels[i].WheelCollider.sidewaysFriction;
            ffriction.asymptoteValue = Wheels[i].WheelCollider.forwardFriction.extremumValue * KeepGrip * 0.998f + 0.002f;
            sfriction.extremumValue = 1f;
            ffriction.extremumSlip = 1f;
            ffriction.asymptoteSlip = 2f;
            ffriction.stiffness = Grip;
            sfriction.extremumValue = 1f;
            sfriction.asymptoteValue = Wheels[i].WheelCollider.sidewaysFriction.extremumValue * KeepGrip * 0.998f + 0.002f;
            sfriction.extremumSlip = 0.5f;
            sfriction.asymptoteSlip = 1f;
            sfriction.stiffness = Grip;
            Wheels[i].WheelCollider.forwardFriction = ffriction;
            Wheels[i].WheelCollider.sidewaysFriction = sfriction;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + centerOfMass.localPosition, .1f);
        Gizmos.DrawWireSphere(transform.position + centerOfMass.localPosition, .11f);
    }

    [System.Serializable]
    public struct WheelInfo
    {
        public WheelCollider WheelCollider;
        public Transform MeshRenderer;
        public bool Steer;
        public bool Motor;
        [HideInInspector]
        public float Rotation;

        public bool isLeftWheel;
    }

    public void resetValues()
    {
        this.rigidbody.velocity = Vector3.zero;
    }
}

