using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    public float speed;
    public float lifeLength;

    private float timer;

    public bool launched;

    private Rigidbody2D rigid;

    public Transform fireworkInVoid;
    private Transform fireworkStand;

    // private GameObject explosionAnimation;
    public Animator animator;

    public CircleCollider2D ccol;

    public bool animationFinished;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        fireworkStand = transform.parent;
        fireworkInVoid = GameObject.Find("FireworkInVoid").transform;
        //explosionAnimation = transform.GetChild(0).gameObject;

    }


    public void Launch()
    {
        if (!launched)
        {
            launched = true;
            transform.parent = null;
            rigid.velocity = transform.up * speed;
            GetComponent<CapsuleCollider2D>().enabled = true;
            // GetComponent<CapsuleCollider2D>().isTrigger = true;
            rigid.isKinematic = false;

        }
    }

    void Update()
    {
        //if (!ccol.enabled)
        //{
        //    Debug.Log("Circle collider disabled");
        //    if (animator.GetBool("Reached"))
        //    {
        //        Debug.Log("Reached ");
        //        animator.SetBool("Reached", false);
        //    }
        //}
        if (animationFinished)
        {
            animator.SetBool("Reached", false);
            GetComponent<SpriteRenderer>().enabled = false;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (launched)
        {
            timer += Time.fixedDeltaTime;
            if (timer > lifeLength)
            {
                Explosion();
            }
        }
    }

    private void Explosion()
    {
        launched = false;
        rigid.velocity = Vector2.zero;
        transform.parent = fireworkInVoid;
        //gameObject.SetActive(false);
        // GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        //
        animator.SetBool("Reached", true);
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        //
    }

    public void Reuse(int number)
    {
        //    if (launched)
        //    {
        launched = false;
        timer = 0;
        rigid.velocity = Vector2.zero;
        transform.rotation = fireworkStand.rotation;
        transform.position = fireworkStand.position + fireworkStand.right * 2.2f * number;
        transform.parent = fireworkStand;

        GetComponent<SpriteRenderer>().enabled = true;

        GetComponent<CapsuleCollider2D>().enabled = false;

        // GetComponent<CapsuleCollider2D>().isTrigger = false;
        //}
        rigid.isKinematic = true;
        rigid.constraints = RigidbodyConstraints2D.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (launched)
        {
            switch (collision.tag)
            {
                case "Obstacle":
                    Explosion();
                    break;
                case "Sheep":
                    Explosion();
                    collision.GetComponent<SheepController>().CollideWithExplosion();
                    break;


                default:
                    //  Debug.Log(" OnTriggerEnter2D  " + collision.name);
                    break;


            }
        }
    }

}
