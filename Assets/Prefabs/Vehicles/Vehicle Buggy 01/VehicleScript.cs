using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/**
 * Class that controls car behaviour, steering and movement
 */
public class VehicleScript : MonoBehaviour
{
    [Header("Engine")]
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    
    [Header("Wheels")]
    public List<Axle> axles;    // List of axles

    [Header("Steer")]
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float horizontalInput, verticalInput;    // Control inputs

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /**
     * Called once per frame
     */
    void Update()
    {

    }

    /**
     * Called several times per frame
     */
    private void FixedUpdate()
    {
        Inputs();
        Steer();
        Accelerate();
    }

    /**
     * Manage input for car control
     */
    private void Inputs()
    {
        // Limit value between -1 and 1
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
        
        // Limit value between 0 and 1
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
    }

    /**
     * Steer wheels
     */
    private void Steer()
    {
        var steeringAngle = maxSteeringAngle * horizontalInput;

        foreach (var axle in axles.Where(axle => axle.canSteer))
        {
            axle.leftWheelCollider.steerAngle = axle.rightWheelCollider.steerAngle = steeringAngle;
            
            UpdateWheelPose(axle.leftWheelCollider, axle.leftWheelTransform);
            UpdateWheelPose(axle.rightWheelCollider, axle.rightWheelTransform);
        }
    }
    
    /**
     * Add torque to wheels
     */
    private void Accelerate()
    {
        var motorTorque = maxMotorTorque * verticalInput;
        
        foreach (var axle in axles.Where(axle => axle.canTorque))
            axle.leftWheelCollider.motorTorque = motorTorque;
    }

    /**
     * Update the a wheel look
     * @collider: Wheel collider
     * @transform: Wheel transform
     */
    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }
    
    [System.Serializable]
    public class Axle {
        public WheelCollider leftWheelCollider, rightWheelCollider;
        public Transform leftWheelTransform, rightWheelTransform;
        public bool canSteer;
        public bool canTorque;
    }
}
