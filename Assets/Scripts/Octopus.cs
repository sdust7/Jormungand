using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    const int numberOfTwo = 8;
    const int numberOfThree = 8;
    private LevelController lvControl;

    private List<Transform> phaseTwoSharks;
    private List<Transform> phaseThreeSharks;
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
        phaseTwoSharks = new List<Transform>();
        phaseThreeSharks = new List<Transform>();
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        for (int i = 0; i < numberOfTwo; i++)
        {
            phaseTwoSharks.Add(transform.GetChild(i).transform);
        }
        for (int i = 0; i < numberOfThree; i++)
        {
            phaseThreeSharks.Add(transform.GetChild(i + 8).transform);
        }
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
                for (int i = 0; i < phaseTwoSharks.Count; i++)
                {
                    phaseTwoSharks[i].gameObject.SetActive(true);
                }
                break;
            case 3:
                for (int i = 0; i < phaseThreeSharks.Count; i++)
                {
                    phaseThreeSharks[i].gameObject.SetActive(true);
                }
                break;
        }
    }

    private void UpdateHP()
    {
        if (HP == 0)
        {
            gameObject.SetActive(false);
            foreach (GameObject shark in GameObject.FindGameObjectsWithTag("Shark"))
            {
                shark.SetActive(false);
            }
            if (lvControl.myQuest.Contains(lvControl.questController.allQuest[3]))
            {
                lvControl.RemoveQuest(lvControl.myQuest.Find(x => x.ID.Equals("3")));
            }
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
            }
            else if (phase == 3)
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
        if (phase == 2)
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
        else if (phase == 3)
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
