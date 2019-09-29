using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCs
{
    ThinWolf = 0,

}

public class SpeakerController : MonoBehaviour
{
    public bool facePlayer;
    private Transform player;

    public int diaglogNumber;

    public DialogController dialogController;

    public NPCs thisNPC;

    public Transform startRoomDoor;

    private LevelController lvControl;
    private bool canBeEaten;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Head").transform;
        dialogController = GameObject.Find("DialogPanel").GetComponent<DialogController>();

        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        canBeEaten = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (facePlayer)
        {
            transform.right = player.position - transform.position;
        }
    }

    public void ThinWolfDead()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //facePlayer = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/DeadWolf");
        canBeEaten = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   // S T A Y   S T A Y   S T A Y   S T A Y   S T A Y   S T A Y   S T A Y 
        if (collision.gameObject.tag == "Snake")
        {
            switch (thisNPC)
            {
                case NPCs.ThinWolf:
                    if (diaglogNumber == 0 && !dialogController.dialogs[0].haveRead)
                    {

                        collision.GetComponent<SnakeController>().canControll = true;
                        dialogController.StartNewDialog(diaglogNumber);
                    }

                    else
                    {
                        switch (transform.childCount)
                        {
                            case 0:
                                diaglogNumber = 3;
                                dialogController.StartNewDialog(diaglogNumber);
                                startRoomDoor.gameObject.SetActive(false);
                                break;
                            case 1:
                                diaglogNumber = 2;
                                dialogController.StartNewDialog(diaglogNumber);
                                startRoomDoor.gameObject.SetActive(false);
                                break;
                            case 2:
                                diaglogNumber = 1;
                                dialogController.StartNewDialog(diaglogNumber);
                                break;
                        }
                    }
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (canBeEaten)
        {

            if (collision.collider.gameObject.tag == "Snake")
            {
                switch (thisNPC)
                {
                    case NPCs.ThinWolf:
                        //gameObject.SetActive(false);
                        lvControl.ExtendBody(4);
                        lvControl.RestoreEnergy(100.0f);
                        diaglogNumber = 4;
                        dialogController.StartNewDialog(diaglogNumber);
                        break;
                }
            }
        }
    }
}

