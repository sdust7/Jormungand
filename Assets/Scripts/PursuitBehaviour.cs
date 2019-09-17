﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PursuitBehaviour : SteeringBehaviour
{
    [SerializeField] protected GameObject targetObj;
    private Vector3 target;
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

    private new void Start()
    {
        predictionTimeBase = predictionTime;
        if (targetObj == null)
        {
            target = transform.position;
        }
        else
        {
            if (targetObj.GetComponent<Rigidbody2D>() == null)
            {
                targetRigidBody = targetObj.GetComponent<Rigidbody2D>();
            }
            else
            {
                targetRigidBody = GameObject.Find("Snake").GetComponent<Rigidbody2D>();
            }
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

    public override void ApplySteering(Rigidbody2D rb)
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
        /*if (dist < 4)
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
        }*/

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
}