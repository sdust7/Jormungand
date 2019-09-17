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
    protected GameObject mapSegments;

    public WolfState state = WolfState.Wandering;
    private CircleCollider2D circleCollider;
    public Rigidbody2D rb; // IMPORTANT: Set this to the parent if AI is in a child object
    LevelController snake;
    private bool full;
    private bool runningCoroutine = false;

    private bool runningScan = false;
    AstarData data;
    GridGraph gg;
    private int width = 50;
    private int depth = 50;
    private float nodeSize = 1;

    MapBounds mapBounds;
    private Bounds totalBound;


    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        pursuit = GetComponent<PursuitBehaviour>();
        wander = GetComponent<WanderBehaviour>();
        mapBounds = GameObject.Find("MapSections").GetComponent<MapBounds>(); 
        snake = GameObject.Find("LevelController").GetComponent<LevelController>(); ;
    }

    private void Start()
    {
        data = AstarPath.active.data;
        gg = data.FindGraphOfType(typeof(GridGraph)) as GridGraph;
        gg.center = new Vector3(0, 0, 0);      
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

    IEnumerator reScan()
    {
        runningScan = true;
        yield return new WaitForSeconds(0.3f);
        gg.SetDimensions(width, depth, nodeSize);
        AstarPath.active.Scan();
        yield return new WaitForSeconds(0.3f);
        runningScan = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (runningScan != true)
        {
            StartCoroutine(reScan());
        }

        totalBound = mapBounds.CalculateLocalBounds();
        width = (int)totalBound.size.x;
        depth = (int)totalBound.size.y;


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
