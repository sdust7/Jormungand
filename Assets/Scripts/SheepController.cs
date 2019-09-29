using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    public enum SheepStatus
    {
        Disabled, Idel, Walk, Escape, Dead//, Avoid, AvoidAndEscape
    };
    public Animator sheepAnimator;
    private Transform snake;
    private LevelController lvControl;
    private Rigidbody2D rigi;

    [SerializeField]
    private SheepStatus currentStatus;
    private SpriteRenderer sprite;
    private Vector2 stop;
    private float movingSpeed;
    private Vector2 escDirection;
    private float escDirectionOffset;
    private float angleToSnake;
    private float timer;

    private Transform fakeTransform;

    public float radius;
    public float distance;
    public LayerMask obstacles;
    public float avoidAngleAccuracy;

    private int frameTimer = 0;

    public float reviveSnakeDis;
    public float reviveTimeWait;
    public float reviveTimer;

    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 2.0f;
        stop = Vector2.zero;
        sprite = transform.GetComponent<SpriteRenderer>();
        rigi = transform.GetComponent<Rigidbody2D>();
        snake = GameObject.Find("SnakeHead").transform;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        sheepAnimator = transform.GetComponent<Animator>();
        currentStatus = SheepStatus.Walk;
        escDirectionOffset = 180;

        fakeTransform = new GameObject().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentStatus == SheepStatus.Dead)
        {
            reviveTimer += Time.fixedDeltaTime;
            if (reviveTimer >= reviveTimeWait)
            {
                if (Vector2.Distance(transform.position, snake.transform.position) >= reviveSnakeDis)
                {
                    Revive();
                }
            }
        }


        frameTimer++;
        if (frameTimer >= 2)
        {
            frameTimer = 0;
            fakeTransform.position = transform.position;
            fakeTransform.up = transform.right;

            switch (currentStatus)
            {
                case SheepStatus.Dead:
                    rigi.velocity = stop;
                    break;
                case SheepStatus.Idel:
                    rigi.velocity = stop;
                    break;
                case SheepStatus.Disabled:
                    rigi.velocity = stop;
                    break;
                case SheepStatus.Walk:
                    if (HasToAvoid(fakeTransform, radius, distance))
                    {
                        rigi.velocity = Vector2.ClampMagnitude(rigi.velocity + V3ToV2(Avoid(fakeTransform, transform.right * movingSpeed, radius, distance)) * 0.1f, movingSpeed);
                        transform.right = rigi.velocity;
                    }
                    else
                    {
                        rigi.velocity = transform.right * movingSpeed;
                    }
                    break;
                case SheepStatus.Escape:
                    if (HasToAvoid(fakeTransform, radius, distance))
                    {
                        rigi.velocity = Vector2.ClampMagnitude(rigi.velocity + V3ToV2(Avoid(fakeTransform, transform.right * movingSpeed * 5.0f, radius, distance)) * 0.2f, movingSpeed * 5.0f);
                        transform.right = rigi.velocity;
                    }
                    {
                        rigi.velocity = transform.right * movingSpeed * 5.0f;
                        escDirection = snake.position - transform.position;
                        if (timer >= 2.0f)
                        {
                            escDirectionOffset = Random.Range(120.0f, 240.0f);
                            timer = 0;
                        }
                        angleToSnake = escDirectionOffset + Mathf.Atan2(escDirection.y, escDirection.x) * Mathf.Rad2Deg;
                        //
                        timer += Time.fixedDeltaTime * 2;
                        //
                        transform.rotation = Quaternion.AngleAxis(angleToSnake, Vector3.forward);
                    }
                    break;
                //case SheepStatus.Avoid:
                //    rigi.velocity = Vector2.ClampMagnitude(rigi.velocity + V3ToV2(Avoid(fakeTransform, transform.right * movingSpeed, radius, distance)) * 0.05f, movingSpeed);
                //    transform.right = rigi.velocity;
                //    break;
                default:
                    break;
            }
        }
    }

    public void StatusChange(SheepStatus status)
    {
        if (currentStatus != SheepStatus.Dead && currentStatus != SheepStatus.Disabled)
        {
            currentStatus = status;
        }
    }

    public void CollideWithSnake()
    {
        transform.GetComponent<PolygonCollider2D>().enabled = false;
        currentStatus = SheepStatus.Dead;
        sheepAnimator.enabled = true;
        // sprite.sprite = Resources.Load<Sprite>("Sprites/DeadSheep");
        lvControl.ExtendBody(4);
        lvControl.RestoreEnergy(100.0f);
    }

    public void CollideWithExplosion()
    {
        if (currentStatus != SheepStatus.Dead && currentStatus != SheepStatus.Disabled)
        {
            currentStatus = SheepStatus.Disabled;
            sprite.sprite = Resources.Load<Sprite>("Sprites/DeadSheep");
        }
    }

    private void Revive()
    {
        transform.GetComponent<PolygonCollider2D>().enabled = true;
        currentStatus = SheepStatus.Walk;
        sheepAnimator.enabled = false;
        sprite.sprite = Resources.Load<Sprite>("Sprites/Sheep");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.gameObject.tag);
        switch (collision.collider.gameObject.tag)
        {
            case "Firework":
                CollideWithExplosion();
                break;
            case "Snake":
                CollideWithSnake();
                break;
        }
        //if (collision.gameObject.tag == "Firework")
        //{
        //    CollideWithExplosion();
        //}
    }

    public bool HasToAvoid(Transform tran, float radius, float distance)
    {
        if (!Physics2D.Raycast(V3ToV2(tran.position - tran.right * radius), tran.up, distance, obstacles))
        {
            if (!Physics2D.Raycast(V3ToV2(tran.position + tran.right * radius), tran.up, distance, obstacles))
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 Avoid(Transform tran, Vector3 velocity, float radius, float distance)
    {
        Vector3 rayStartPosiLeft = tran.position - tran.right * radius + tran.up * radius;
        Vector3 rayStartPosiRight = tran.position + tran.right * radius + tran.up * radius;
        bool leftIsClear;
        bool rightIsClear;
        bool turnLeft = false;

        float angleLeft = 0;
        float angleRight = 0;
        Vector3 rotatedLeft = tran.up;
        Vector3 rotatedRight = tran.up;

        while (angleLeft < 180)
        {
            rotatedLeft = Quaternion.Euler(0, 0, angleLeft) * rotatedLeft;
            rotatedRight = Quaternion.Euler(0, 0, angleRight) * rotatedRight;

            Debug.DrawLine(rayStartPosiLeft, rayStartPosiLeft + rotatedLeft * distance, Color.red);
            Debug.DrawLine(rayStartPosiRight, rayStartPosiRight + rotatedRight * distance, Color.red);

            leftIsClear = true;
            rightIsClear = true;

            if (Physics2D.Raycast(rayStartPosiLeft, rotatedLeft, distance, obstacles))
            {
                leftIsClear = false;
                angleLeft += avoidAngleAccuracy;
            }
            if (Physics2D.Raycast(rayStartPosiRight, rotatedRight, distance, obstacles))
            {
                rightIsClear = false;
                angleRight -= avoidAngleAccuracy;
            }

            if (leftIsClear)
            {
                turnLeft = true;
                break;
            }
            if (rightIsClear)
            {
                turnLeft = false;
                break;
            }
        }

        if (turnLeft)
        {
            while (angleLeft < 179)
            {
                rotatedLeft = Quaternion.Euler(0, 0, angleLeft) * rotatedLeft;
                if (Physics2D.Raycast(rayStartPosiRight, rotatedLeft, distance, obstacles))
                {
                    angleLeft += avoidAngleAccuracy;
                }
                else
                {
                    return Quaternion.Euler(0, 0, angleLeft) * rotatedLeft;
                }
            }
        }
        else
        {
            while (angleRight > -179)
            {
                rotatedRight = Quaternion.Euler(0, 0, angleRight) * rotatedRight;
                if (Physics2D.Raycast(rayStartPosiLeft, rotatedRight, distance, obstacles))
                {
                    angleRight -= avoidAngleAccuracy;
                }
                else
                {
                    return Quaternion.Euler(0, 0, angleRight) * rotatedRight;
                }
            }
        }
        return -tran.right;
    }

    public Vector2 V3ToV2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }
}
