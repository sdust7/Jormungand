using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    public enum SheepStatus
    {
        Rest, Idel, Walk, Escape,Dead
    };
    public Animator sheepAnimator;
    private Transform snake;
    private LevelController lvControl;
    private Rigidbody2D rigi;
    private SheepStatus currentStatus;
    private SpriteRenderer sprite;
    private Vector2 stop;
    private float movingSpeed;
    private Vector2 escDirection;
    private float escDirectionOffset;
    private float angleToSnake;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 2.0f;
        stop = new Vector2(0, 0);
        sprite = transform.GetComponent<SpriteRenderer>();
        rigi = transform.GetComponent<Rigidbody2D>();
        snake = GameObject.Find("SnakeHead").transform;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        sheepAnimator = transform.GetComponent<Animator>();
        currentStatus = SheepStatus.Walk;
        escDirectionOffset = 180;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentStatus)
        {
            case SheepStatus.Dead:
                rigi.velocity = transform.right * stop;
                break;
            case SheepStatus.Idel:
                rigi.velocity = stop;
                break;
            case SheepStatus.Rest:
                break;
            case SheepStatus.Walk:
                rigi.velocity = transform.right * movingSpeed;
                break;
            case SheepStatus.Escape:
                rigi.velocity = transform.right * movingSpeed*5.0f;
                escDirection = snake.position - transform.position;
                if (timer >= 2.0f)
                {
                    escDirectionOffset = Random.Range(120.0f, 240.0f);
                    timer = 0;
                }
                angleToSnake =  escDirectionOffset+ Mathf.Atan2(escDirection.y, escDirection.x) * Mathf.Rad2Deg;
                timer+=Time.fixedDeltaTime;
                transform.rotation = Quaternion.AngleAxis(angleToSnake, Vector3.forward);
                break;
            default:
                break;
        }
    }

    public void StatusChange(SheepStatus status)
    {
        if (currentStatus != SheepStatus.Dead)
        {
            currentStatus = status;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            transform.GetComponent<PolygonCollider2D>().enabled = false;
            currentStatus = SheepStatus.Dead;
            sheepAnimator.enabled = true;
            sprite.sprite = Resources.Load<Sprite>("Sprites/DeadSheep");
            lvControl.ExtendBody(4);

        }

    }
}
