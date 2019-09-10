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
    SnakeController snake = new SnakeController();
    private bool full;
    private bool runningCoroutine = false;

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pursuit = GetComponent<PursuitBehaviour>();
    }

    IEnumerator eatingFull()
    {
        //Toggle bool to prevent multiple instances of coroutine running at the same time
        runningCoroutine = true;
        yield return new WaitForSeconds(3);
        full = pursuit.ToggleHunger(full);
        runningCoroutine = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        pursuit.ApplySteering(rb);

        //check if already running coroutine
        if (runningCoroutine != true)
        {
            StartCoroutine(eatingFull());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Track player if nearby
        if (collision.gameObject.name == "Snake")
        {
            print("detected snake: " + collision.name);
            if (targetObj == this.gameObject || collision.gameObject.name == "Snake")
            {
                targetObj = collision.gameObject;
                pursuit.SetTargetObj(targetObj);
            }
        }
        //Track self as a substitute for null targets
        else
        {
            targetObj = this.gameObject;
            pursuit.SetTargetObj(targetObj);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (full)
            return;
        //Delete player if not full
        else if (collision != null && collision.gameObject.name == "Snake")
        {
            full = pursuit.ToggleHunger(full);
            print("Snake hit");
            snake.GotDamage(10.0f);
        }
    }

    public void SetState(int stateNumber)
    {
        state = (WolfState)stateNumber;
    }
}
