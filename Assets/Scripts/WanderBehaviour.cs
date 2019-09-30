using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : SteeringBehaviour
{
    [SerializeField] private float circleRadius; // Displacement circle radius
    [SerializeField] private float circleDistance; // Distance of circle centre from object
    [SerializeField] private float angleChangeTime;
    private float angle;
    private bool changeAngle;
    private Vector3 circleCentre, displacement, steering;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        angle = 0;
        changeAngle = true;
    }

    public override void ApplySteering(Rigidbody2D rb)
    {
        // Calculate circle centre along the direction of the velocity
        circleCentre = Vector3.Normalize(rb.velocity) * circleDistance;
        displacement = new Vector3(0, circleRadius, 0);

        // Apply random rotation to displacement force
        if (changeAngle)
        {
            StartCoroutine(ChangeAngle(angleChangeTime));
        }

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        displacement = rotation * new Vector3(0, circleRadius, 0);

        // Apply the steering force
        steering = circleCentre + displacement;
        rb.AddForce(steering * weight);

        // Clamp the speed
        ClampSpeed(rb);

        // Set orientation
        SetOrientationToVelocity(rb);
    }

    private IEnumerator ChangeAngle(float timeToWait)
    {
        changeAngle = false;
        angle = Random.Range(0, 360);
        // Wait for n seconds before continuing
        yield return new WaitForSeconds(timeToWait);
        changeAngle = true;
    }

}