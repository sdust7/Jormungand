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

        pursuit.ApplySteering(rb);

        //check if already running coroutine
        if (runningCoroutine != true)
        {
            StartCoroutine(eatingFull());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Track fish and player if nearby
        if (collision != null && (collision.gameObject.layer == 9 || collision.gameObject.name == "Player"))
        {
            if (targetObj == this.gameObject || collision.gameObject.name == "Player")
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

        //Delete fish if not full
        if (collision != null && collision.gameObject.layer == 9)
        {
            full = pursuit.ToggleHunger(full);
            Destroy(collision.gameObject);
        }
        //Delete player if not full
        else if (collision != null && collision.gameObject.layer == 8)
        {
            full = pursuit.ToggleHunger(full);
            Destroy(collision.gameObject);
        }
    }

    public void SetState(int stateNumber)
    {
        state = (WolfState)stateNumber;
    }
}
