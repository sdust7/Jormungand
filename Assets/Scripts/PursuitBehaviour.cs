using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PursuitBehaviour : MonoBehaviour
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
