using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    public AudioSource forestMusic;
    public AudioSource bossBattleMusic;

    public bool isInBossBattle;
    //public float switchMusicTime;
    //public float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInBossBattle)
        {
            if (forestMusic.volume < 1.0f)
            {
                forestMusic.volume += 0.005f;
            }
            if (bossBattleMusic.volume > 0)
            {
                bossBattleMusic.volume -= 0.005f;
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            isInBossBattle = true;
            if (forestMusic.volume > 0)
            {
                forestMusic.volume -= 0.005f;
            }
            if (bossBattleMusic.volume < 1.0f)
            {
                bossBattleMusic.volume += 0.005f;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Snake")
    //    {

    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            isInBossBattle = false;

        }
    }

}
