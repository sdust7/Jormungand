using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyController : MonoBehaviour
{
    private LevelController lvControl;

    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            lvControl.DamageSnake(5.0f);
        }
    }
}
