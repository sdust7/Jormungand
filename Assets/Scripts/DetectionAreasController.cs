using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAreasController : MonoBehaviour
{
    private SheepController sheep;
    // Start is called before the first frame update
    void Start()
    {
        sheep = transform.parent.GetComponent<SheepController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake"&&transform.name=="AlertArea")
        {
            sheep.StatusChange(SheepController.SheepStatus.Escape);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Snake" && transform.name == "SafeArea")
        {
            sheep.StatusChange(SheepController.SheepStatus.Walk);
        }
    }
}
