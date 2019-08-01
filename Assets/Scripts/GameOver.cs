using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GlobalController global;

    // Start is called before the first frame update
    void Start()
    {
        global = GameObject.Find("GlobalController").GetComponent<GlobalController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Head")
        {
            global.GameOver();
        }
        else if (collider.name == "Body(Clone)" && transform.name != "Body(Clone)")
        {
            global.GameOver();
        }

    }

}
