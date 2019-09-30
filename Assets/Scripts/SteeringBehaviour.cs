using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] protected float maxSpeed = 2; // The speed limit for this object
    protected Vector3 desiredVelocity;
    protected float orientation;    // Angle in degrees
    protected float speed;          // Stores the speed of the object
    public float weight = 1;     // Scale of the steering force

    protected void Start()
    {
        desiredVelocity = Vector3.zero;
        speed = maxSpeed;
    }

    protected Vector3 GetSteeringVelocity(Vector3 desired, Vector3 current)
    {
        Vector3 linear = desired - current;
        return linear;
    }

    // Get the orientation in degrees for an object facing in the velocity's direction
    protected float GetOrientation(Vector3 velocity)
    {
        if (velocity.magnitude > 0)
        {
            float angle = Mathf.Atan2(-velocity.x, velocity.y);
            return angle * Mathf.Rad2Deg;
        }
        else
        {
            // If no velocity, don't change the orientation
            return orientation;
        }
    }

    public void SetWeight(float weight)
    {
        this.weight = weight;
    }

    public void SetOrientationToVelocity(Rigidbody2D rb)
    {
        orientation = GetOrientation(rb.velocity);
        rb.rotation = orientation;
    }

    public void ClampSpeed(Rigidbody2D rb)
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
    }

    public abstract void ApplySteering(Rigidbody2D rb);
}
