using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PursuitBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject targetObj;
    private Vector3 target;

    protected float speed;
    public float maxSpeed;
    protected Vector3 desiredVelocity;

    protected float orientation;
    public float weight = 1;

    [SerializeField] private float predictionTime;
    private float predictionTimeBase;
    [SerializeField] protected float arrivalRadius;

    [SerializeField] private float nextWayPointDist;

    Path path;
    int currentWayPoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    public Rigidbody2D rigidBod;

    private CircleCollider2D circleCollider;
    private Rigidbody2D targetRigidBody;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        predictionTimeBase = predictionTime;
        if (targetObj == null)
        {
            target = transform.position;
        }
        else
        {
            targetRigidBody = targetObj.GetComponent<Rigidbody2D>();
        }
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBod.position, target, OnPathCompelte);
        }
    }

    void OnPathCompelte(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }

    }

    public void ApplySteering(Rigidbody2D rb)
    {
        // Return if no target
        if (targetObj == null)
            return;

        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        target = targetObj.transform.position;
        float dist2ChkP = Vector2.Distance(rigidBod.position, path.vectorPath[currentWayPoint]);
        float dist = Vector3.Distance(target, transform.position);
        if (dist < 4)
        {
            if (predictionTime > 0.25f)
            {
                predictionTime -= 0.1f;
            }
            else if (predictionTime < 0.25f)
            {
                predictionTime = 0.25f;
            }
        }
        else
        {
            predictionTime = predictionTimeBase;
        }

        // Get predicted target position after [predictionTime] seconds
        if (targetRigidBody != null)
            target += (Vector3)(targetRigidBody.velocity * predictionTime);

        // Get distance and target vector
        Vector3 targetOffset = ((Vector2)path.vectorPath[currentWayPoint] - rigidBod.position).normalized;
        dist = Vector3.Distance(target, transform.position);
        if (dist2ChkP < nextWayPointDist)
        {
            currentWayPoint++;
        }
        // Return if target is not within collider
        if (dist > circleCollider.radius)
            return;

        // Get desired velocity
        desiredVelocity = targetOffset * maxSpeed;
        Vector3 linear = GetSteeringVelocity(desiredVelocity, rb.velocity);

        // Apply steering and clamp to maximum speed
        rb.AddForce(linear * weight);

        if (dist < arrivalRadius && arrivalRadius > 0)
            speed = maxSpeed * (dist / arrivalRadius);
        else
            speed = maxSpeed;

        // Clamp the speed
        ClampSpeed(rb);

        // Set orientation
        SetOrientationToVelocity(rb);
    }

    public void SetTargetObj(GameObject targetObj)
    {
        this.targetObj = targetObj;
        targetRigidBody = targetObj.GetComponent<Rigidbody2D>();
    }

    public bool ToggleHunger(bool full)
    {
        if (full)
        {
            full = false;
        }
        else
        {
            full = true;
        }
        return full;
    }

    protected Vector3 GetSteeringVelocity(Vector3 desired, Vector3 current)
    {
        Vector3 linear = desired - current;
        return linear;
    }

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

    public void SetOrientationToVelocity(Rigidbody2D rb)
    {
        orientation = GetOrientation(rb.velocity);
        rb.rotation = orientation;
    }

    public void ClampSpeed(Rigidbody2D rb)
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
    }
}
