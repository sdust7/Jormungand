using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private Animator MenuSnakeAni;
    public GameObject singlePlay;
    public GameObject multiPlay;
    public GameObject lvObject;
    public Transform questPanel;
    public Transform submitPanel;
    private LevelController lvControl;

    public GameObject snake;

    public GameObject energyBar;


    // Start is called before the first frame update
    void Start()
    {
        lvControl = lvObject.GetComponent<LevelController>();
        if (singlePlay != null)
            MenuSnakeAni = GameObject.Find("MenuSnakeAni").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DebugEnableSpeedUp()
    {
        lvControl.SnakeCanSpeedUp(true);
        energyBar.SetActive(true);
    }

    public void DebugMoveSnake()
    {
        lvControl.SnakeRespawn();
    }

    public void DebugEnableAxe()
    {
        snake.GetComponent<SnakeController>().AddEquipment(Equipments.Axe);
    }

    public void DebugEnableFirework()
    {
        snake.GetComponent<SnakeController>().AddEquipment(Equipments.FireworkStand);
    }


    public void DebugGotItemSlot1()
    {
        GameObject.Find("ToolBar").GetComponent<ToolBar>().GotItem(new Items("EnergyPotion", true, 1));
    }

    public void DebugGotItemSlot2()
    {
        GameObject.Find("ToolBar").GetComponent<ToolBar>().GotItem(new Items("HealthPotion", true, 1));
    }

    public void DebugGotItemSlot3()
    {
        GameObject.Find("ToolBar").GetComponent<ToolBar>().GotItem(new Items("SheepBone", false, 1), 10);
    }

    public void Restart()
    {
        lvControl.SnakeRespawn();

        //SceneManager.LoadScene("SampleScene");
        //Time.timeScale = 1;
    }

    IEnumerator WaitForSnake(float time)
    {
        yield return new WaitForSeconds(time);
        singlePlay.SetActive(true);
        multiPlay.SetActive(true);
    }

    public void ClickStart()
    {
        if (!MenuSnakeAni.enabled)
        {
            MenuSnakeAni.enabled = true;
            StartCoroutine(WaitForSnake(0.6f));
        }


    }

    public void ComfirmQuest()
    {

        int id = int.Parse(questPanel.GetChild(0).name);
        Quest quest = lvControl.questController.allQuest[id];
        //lvControl.myQuest.Add(quest);
        lvControl.AddQuest(quest);

        questPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (id == 2)
        {
            lvControl.RemoveQuest(lvControl.myQuest[0]);
        }
        if (quest.showMapMark)
        {
            lvControl.ShowMiniMapMark(quest.targetTrans.position);
        }
    }

    public void BackQuest()
    {
        questPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void CancelSubmit()
    {
        submitPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SubmitQuest()
    {

        lvControl.SubmitQuest(submitPanel.GetChild(0).name);
        submitPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }



    public void ClickSingle()
    {
        SceneManager.LoadScene("SampleScene");
    }



}
