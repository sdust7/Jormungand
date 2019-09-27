using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour
{
    public bool facePlayer;
    private Transform player;

    public int diaglogNumber;

    public DialogController dialogController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Head").transform;
        dialogController = GameObject.Find("DialogPanel").GetComponent<DialogController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facePlayer)
        {
            transform.right = player.position - transform.position;
        }
    }

    private void OnTriggerEnter2D()
    {
        dialogController.StartNewDialog(diaglogNumber);
    }

}
