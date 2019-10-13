using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    private LevelController lvControl;

    public int aliveShark;
    private Transform center;
    public int phase;
    public int shieldHP;
    private bool damaged;
    private float damageTimer;
    private Animator animator;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();

        HP = 15;
        animator = transform.GetComponent<Animator>();
        damageTimer = 0;
        damaged = false;
        shieldHP = 15;
        aliveShark = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageTimer += Time.deltaTime;
        }
        if (damageTimer >= 1.0f)
        {
            damaged = false;
            damageTimer = 0;
        }
    }
    private void PhaseChange()
    {
        switch (phase)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

    }
    private void UpdateHP()
    {
        if (HP == 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void SharkDead()
    {
        aliveShark--;
        if (aliveShark == 0)
        {
            phase = 2;
            PhaseChange();
        }
    }

    private void UpdateShield()
    {
        if (shieldHP < 7)
        {
            animator.SetInteger("Phase", 2);
        }
        if (shieldHP == 0)
        {
            phase = 3;
            animator.SetInteger("Phase", 3);
            PhaseChange();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "AXE" && damaged == false)
        {
            if (phase == 2)
            {
                shieldHP -= 1;
                if (shieldHP < 0)
                {
                    shieldHP = 0;
                }
                UpdateShield();
                damaged = true;
            }else if (phase == 3)
            {
                HP -= 1;
                if (HP < 0)
                {
                    HP = 0;
                }
                UpdateShield();
                damaged = true;

            }
        }
        else if (collision.collider.gameObject.tag == "Snake")
        {
            lvControl.DamageSnake(15);
        }
    }

    public void CollideWithExplosion(bool wave)
    {
        if (phase ==2)
        {
            if (wave)
            {
                shieldHP -= 1;
                if (shieldHP < 0)
                {
                    shieldHP = 0;
                }
                //damaged = true;
                UpdateShield();


            }
            else
            {
                shieldHP -= 2;
                if (shieldHP < 0)
                {
                    shieldHP = 0;
                }
                //damaged = true;
                UpdateShield();
            }
        }
        else if(phase == 3)
        {
            if (wave)
            {
                HP -= 1;
                if (HP < 0)
                {
                    HP = 0;
                }
                UpdateHP();
            }
            else
            {
                HP -= 2;
                if (HP < 0)
                {
                    HP = 0;
                }
                UpdateHP();
            }

        }
    }
}
