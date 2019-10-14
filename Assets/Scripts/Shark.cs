using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private LevelController lvControl;
    public float steeringSpeed;
    public float speed;
    private Rigidbody2D rigid;
    private Transform center;
    private int HP;
    public Sprite[] damageSprites;
    private bool damaged;
    private float damageTimer;
    private Octopus octopus;
    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        octopus = GameObject.Find("Octopus").GetComponent<Octopus>();
        damageTimer=0;
        damaged = false;
        HP = 6;
        center = GameObject.Find("Octopus").transform;
        rigid = transform.GetComponent<Rigidbody2D>();
        //rigid.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(Vector2.Distance(transform.position, center.position));

        //if (Vector2.Distance(transform.position, center.position) < 45)
        //{
        //    transform.Translate((transform.position - center.position)*Time.fixedDeltaTime);
        //}
        //else
        //{

        if (damaged)
        {
            damageTimer += Time.fixedDeltaTime;
        }
        if (damageTimer >= 1.0f)
        {
            damaged = false;
            damageTimer = 0;
        }
        if (HP > 0)
        {

            transform.Rotate(0, 0, steeringSpeed);
        }
        else
        {
            if (speed > 0)
            {
                speed -= Time.fixedDeltaTime*5;
            }
        }
        rigid.velocity = transform.right * speed;

        ////rigid.velocity += Vector2.ClampMagnitude(V3ToV2(center.position - transform.position), steeringSpeed);
        ////rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, speed);
        //transform.up = center.position - transform.position;
        ////transform.right = rigid.velocity;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //rigid.velocity = transform.right * speed;
        //}

    }

    private void UpdateDamge()
    {
        switch (HP)
        {
            case 0:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[6];
                octopus.SharkDead();
                break;
            case 1:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[5];

                break;
            case 2:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[4];
                break;
            case 3:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[3];

                break;
            case 4:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[2];

                break;
            case 5:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[1];

                break;
            case 6:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[0];

                break;
            default:
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[6];
                octopus.SharkDead();

                break;
        }
    }
    public void CollideWithExplosion(bool wave)
    {
        if (damaged == false)
        {
            if (wave)
            {
                HP -= 2;
                damaged = true;
            }
            else
            {
                HP -= 3;
                damaged = true;
            }
            UpdateDamge();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "AXE"&&damaged==false)
        {
            HP -= 1;
            UpdateDamge();
            damaged = true;
        }else if (collision.collider.gameObject.tag == "Snake")
        {
            if (HP <= 0)
            {
                lvControl.RestoreSnakeHealth(100);
                transform.GetComponent<SpriteRenderer>().sprite = damageSprites[7];
                transform.GetComponent<PolygonCollider2D>().enabled = false;
            }
            else
            {
                lvControl.DamageSnake(10);
            }
        }
    }

}
