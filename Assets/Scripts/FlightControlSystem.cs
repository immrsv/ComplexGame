﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FlightControlSystem : MonoBehaviour {

    [System.Serializable]
    public class ThrustPotential {
        public float Prograde = 1;
        public float Retrograde = 1;
        public float Horizontal = 1;
        public float Vertical = 1;
    }

    public ThrustPotential ThrusterOutput;
    public PhysicsIntent DesiredMotion;

    public float MaxSpeed { get; set; }

    private Rigidbody rb;

    public Vector3 Drift;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        MaxSpeed = 10.0f;
    }

	// Update is called once per physics frame
	void FixedUpdate () {

        // MotionIntents are in Inertial space, but Rigidbody wants world-space
        var desiredWorldMotion = new PhysicsIntent {
            Linear = GetComponent<Rigidbody>().transform.TransformDirection(DesiredMotion.Linear),
            //Angular = GetComponent<Rigidbody>().transform.TransformDirection(DesiredMotion.Angular)
            Angular = DesiredMotion.Angular
        };


        var linearDeltaV = desiredWorldMotion.Linear - rb.velocity; // Velocity difference between current and desired
        var angularDeltaV = desiredWorldMotion.Angular - rb.angularVelocity.y;

        // TODO: should use smoothstep to increase low-delta thrust
        var accel = new PhysicsIntent { Linear = linearDeltaV, Angular = angularDeltaV };

        //rb.AddRelativeForce(accel.Linear, ForceMode.Acceleration);
        //rb.AddRelativeTorque(accel.Angular, ForceMode.Acceleration);

        rb.velocity = Vector3.ClampMagnitude(desiredWorldMotion.Linear, MaxSpeed);
        rb.angularVelocity = new Vector3(0, desiredWorldMotion.Angular, 0);
        
        rb.velocity += Drift;
    }
}
