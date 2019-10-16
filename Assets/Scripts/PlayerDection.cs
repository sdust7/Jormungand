using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDection : MonoBehaviour
{

    public GameObject octopusField;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            octopusField.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            octopusField.SetActive(false);
        }
    }

}
