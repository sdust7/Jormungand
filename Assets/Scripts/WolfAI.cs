using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    public enum WolfState { Wandering, Hunting, Eating };

    private PursuitBehaviour pursuit;
    protected GameObject targetObj;

    public WolfState state = WolfState.Wandering;
    private CircleCollider2D circleCollider;
    public Rigidbody2D rb; // IMPORTANT: Set this to the parent if AI is in a child object
    LevelController snake;
    private bool full;
    private bool runningCoroutine = false;
    private bool runningScan = false;


    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pursuit = GetComponent<PursuitBehaviour>();
        snake = GameObject.Find("LevelController").GetComponent<LevelController>(); ;
    }

    IEnumerator eatingFull()
    {
        //Toggle bool to prevent multiple instances of coroutine running at the same time
        runningCoroutine = true;
        yield return new WaitForSeconds(3);
        full = pursuit.ToggleHunger(full);
        runningCoroutine = false;
    }

    IEnumerator reScan()
    {
        runningScan = true;
        yield return new WaitForSeconds(0.1f);
        AstarPath.active.Scan();
        //yield return new WaitForSeconds(2);
        runningScan = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
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
                //wander.ApplySteering(rb);
                break;
            case WolfState.Hunting:
                pursuit.ApplySteering(rb);
                break;
            case WolfState.Eating:
                //wander.ApplySteering(rb);
                //check if already running coroutine
                if (runningCoroutine != true)
                {
                    StartCoroutine(eatingFull());
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Track player if nearby
        //print(collision.gameObject.name);
        if (collision != null && (collision.gameObject.layer == 10 || collision.gameObject.name == "Head"))
        {
            if (targetObj == this.gameObject || collision.gameObject.name == "Head")
            {
                targetObj = collision.gameObject;
                pursuit.SetTargetObj(targetObj);
            }
        }
        //Track self as a substitute for null targets
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.name == "Head")
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
        if (collision != null && collision.gameObject.name == "Snake")
        {
            full = pursuit.ToggleHunger(full);
            print("Snake hit");
            snake.DamageSnake(10.0f);
        }
    }

    public void SetState(int stateNumber)
    {
        state = (WolfState)stateNumber;
    }
}
