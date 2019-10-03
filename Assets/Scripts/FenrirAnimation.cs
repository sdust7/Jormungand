using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirAnimation : MonoBehaviour
{
    public GameObject[] anime;
    public float[] animationLength;
    private bool[] played;


    private bool isPlaying;
    private float playTimer;
    private int currentAnimation;

    private void Start()
    {
        played = new bool[anime.Length];
        currentAnimation = 0;
        for (int i = 0; i < played.Length; i++)
        {
            played[i] = false;
        }
        playTimer = 0;
    }

    void Update()
    {
        if (isPlaying)
        {
            playTimer += Time.deltaTime;
            if (playTimer >= animationLength[currentAnimation])
            {
                Time.timeScale = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
        {
            for (int i = 0; i < played.Length; i++)
            {
                if (played[i] == false)
                {
                    anime[i].SetActive(true);
                    played[i] = true;
                    Time.timeScale = 0;
                    isPlaying = true;
                    currentAnimation = i;
                    break;
                }
            }
        }
    }
}
