using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestProvider : MonoBehaviour
{
    private LevelController lvControl;
    private Quest myQuest;
    private Transform submitPanel;

    private void Awake()
    {
        submitPanel = GameObject.Find("MissionPanel").transform.GetChild(1).transform;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        // myQuest = lvControl.questController.allQuest[Random.Range(0, lvControl.questController.allQuest.Count)];

    }
    // Start is called before the first frame update
    void Start()
    {
        myQuest = lvControl.questController.allQuest[0];
        print(myQuest.questName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool hasQuest(Quest myQuest)
    {
      return  lvControl.myQuest.Exists(x => x.ID.Equals(myQuest.ID));
    }

    private void InteractiveWithSnake()
    {
        if (!myQuest.finished)
        {
            if (hasQuest(myQuest))
            {
                submitPanel.gameObject.SetActive(true);
                submitPanel.GetChild(0).name = myQuest.ID;
                submitPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Do you want to submit quest: " + myQuest.questName + "?";
                Time.timeScale = 0;

            }
            else
            {
                lvControl.ShowMissionPanel(myQuest);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
        {
            InteractiveWithSnake();
        }
    }
}
