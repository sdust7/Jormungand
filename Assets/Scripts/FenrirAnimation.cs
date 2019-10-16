using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirAnimation : MonoBehaviour
{
    public GameObject[] anime;
    public float[] animationLength;
    private bool[] played;


    private bool isPlaying;
    [SerializeField]
    private float playTimer;
    private int currentAnimation;

    public GameObject energyBar;

    private LevelController lvController;

    private void Start()
    {
        played = new bool[anime.Length];
        currentAnimation = 0;
        for (int i = 0; i < played.Length; i++)
        {
            played[i] = false;
        }
        playTimer = 0;

        lvController = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    void Update()
    {
        if (isPlaying)
        {
            playTimer += Time.unscaledDeltaTime;
            if (playTimer >= animationLength[currentAnimation])
            {
                Time.timeScale = 1;
                anime[currentAnimation].SetActive(false);
                played[currentAnimation] = true;
                isPlaying = false;
                if (currentAnimation == 0)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    lvController.SnakeCanSpeedUp(true);
                    energyBar.SetActive(true);

                    lvController.RemoveQuest(lvController.myQuest[0]);

                    lvController.currentCheckPoint = new Vector3(-25, -260, 0);
                }
                if (currentAnimation < anime.Length - 1)
                {
                    currentAnimation++;
                }
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
