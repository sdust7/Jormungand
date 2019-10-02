using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirAnimation : MonoBehaviour
{
    public GameObject[] anime;
    private bool[] played;

    private void Start()
    {
        played = new bool[anime.Length];
        for (int i = 0; i < played.Length; i++)
        {
            played[i] = false;
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
                    break;
                }
            }
        }
    }
}
