using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WolfAI : MonoBehaviour
{
    public enum WolfState { Wandering, Hunting, Eating };

    private PursuitBehaviour pursuit;
    private WanderBehaviour wander;
    protected GameObject targetObj;

    public WolfState state = WolfState.Wandering;
    private CircleCollider2D circleCollider;
    public Rigidbody2D rb; // IMPORTANT: Set this to the parent if AI is in a child object
    LevelController lvController;
    private bool full;
    private bool sheepEaten;
    private bool runningCoroutine = false;

    private bool runningScan = false;
    AstarData data;
    GridGraph gg;
    private int width = 50;
    private int depth = 50;
    private float nodeSize = 1;
    ////
    private Transform snakeTrans;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pursuit = GetComponent<PursuitBehaviour>();
        wander = GetComponent<WanderBehaviour>();
        lvController = GameObject.Find("LevelController").GetComponent<LevelController>(); ;
        ////
        snakeTrans = GameObject.Find("Head").transform;
    }

    private void Start()
    {
        data = AstarPath.active.data;
        gg = data.FindGraphOfType(typeof(GridGraph)) as GridGraph;
        gg.SetDimensions(width, depth, nodeSize);
    }

    IEnumerator eatingFull()
    {
        //Toggle bool to prevent multiple instances of coroutine running at the same time
        runningCoroutine = true;
        yield return new WaitForSeconds(3);
        full = pursuit.ToggleHunger(full);
        runningCoroutine = false;
    }

    IEnumerator sheepEatingFull()
    {
        //Toggle bool to prevent multiple instances of coroutine running at the same time
        runningCoroutine = true;
        yield return new WaitForSeconds(10);
        full = pursuit.ToggleHunger(full);
        runningCoroutine = false;
    }

    IEnumerator reScan()
    {
        runningScan = true;
        yield return new WaitForSeconds(0.3f);
        AstarPath.active.Scan();
        yield return new WaitForSeconds(0.3f);
        runningScan = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //  Debug.Log(targetObj + "      " + state);
        if (transform.position.x >= lvController.xValueStartDesert - 20.0f || transform.position.x <= lvController.xValueStartSea + 20.0f)
        {
            transform.right = new Vector2(-transform.position.x, 0);
            //rb.velocity = transform.right * 5;
            rb.AddForce(transform.right * 100);
        }

        gg.center = rb.transform.localPosition;
        if (runningScan != true)
        {
            StartCoroutine(reScan());
        }
        //print(targetObj);
        if (targetObj == this.gameObject)
        {
            state = WolfState.Wandering;
        }
        else if (full)
        {
            state = WolfState.Eating;
        }
        else
        {
            state = WolfState.Hunting;
        }

        switch (state)
        {
            case WolfState.Wandering:
                wander.ApplySteering(rb);
                break;
            case WolfState.Hunting:
                pursuit.ApplySteering(rb);
                break;
            case WolfState.Eating:
                wander.ApplySteering(rb);
                //check if already running coroutine
                if (runningCoroutine != true)
                {
                    if (full)
                    {
                        StartCoroutine(eatingFull());
                    }
                    else if (sheepEaten)
                    {
                        StartCoroutine(sheepEatingFull());
                    }
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Track player if nearby
        //print(collision.gameObject.name);
        //if (collision.gameObject.layer == 10 || collision.gameObject.tag == "Snake")
        //{
        //    if (targetObj == this.gameObject || collision.gameObject.tag == "Snake")
        //    {
        //        targetObj = collision.gameObject;
        //        pursuit.SetTargetObj(targetObj);
        //    }
        //}
        //Track self as a substitute for null targets
        ////
        if (collision.gameObject.tag == "Snake")
        {
            if (Vector2.Distance(collision.transform.position, transform.position) > 13.0f)
            {
                targetObj = collision.gameObject;
                pursuit.SetTargetObj(targetObj);
            }
            else
            {
                targetObj = this.gameObject;
                pursuit.SetTargetObj(targetObj);
            }
        }
        else if (collision.gameObject.layer == 10)
        {
            if (Vector2.Distance(snakeTrans.position, transform.position) <= 13.0f)
            {
                targetObj = collision.gameObject;
                pursuit.SetTargetObj(targetObj);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.tag == "Snake")
        {
            targetObj = this.gameObject;
            pursuit.SetTargetObj(targetObj);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (full)
            return;
        //Damage player if not full
        if (collision != null && collision.gameObject.tag == "Snake")
        {
            full = pursuit.ToggleHunger(full);
            // lvController.DamageSnake(10.0f);
        }
        if (collision.gameObject.tag == "Sheep")
        {
            sheepEaten = pursuit.ToggleHunger(sheepEaten);
        }
    }

    public void SetState(int stateNumber)
    {
        state = (WolfState)stateNumber;
    }
}
