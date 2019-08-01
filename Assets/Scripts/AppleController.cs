using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : MonoBehaviour
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
            global.GetApple();
            int x = Random.Range(0, 19);
            int z = Random.Range(0, 19);
            transform.position = new Vector3(x + 0.5f, 0.5f, z + 0.5f);
        }
    }



}
