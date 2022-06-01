using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/**
 * Class that controls car behaviour
 */
public class VehicleScript : MonoBehaviour
{
    [Header("Engine")]
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    
    [Header("Wheels")]
    public List<Axle> axles;    // List of axles

    [Header("Steer")]
    public float maxSteeringAngle, horizontalMoveInput; // maximum steer angle the wheel can have

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
        Steer();
        Accelerate();
    }

    /**
     * Steer wheels
     */
    private void Steer()
    {
        var steeringAngle = maxSteeringAngle * horizontalMoveInput;

        foreach (var axle in axles.Where(axle => axle.canSteer))
        {
            axle.leftWheelCollider.steerAngle = axle.rightWheelCollider.steerAngle = steeringAngle;
            
            UpdateWheelPose(axle.leftWheelCollider, axle.leftWheelTransform);
            UpdateWheelPose(axle.rightWheelCollider, axle.rightWheelTransform);
        }
    }
    
    private void Accelerate()
    {
        
    }
    
    /**
     * Update the a wheel look
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
    }
}
