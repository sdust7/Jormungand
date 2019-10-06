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




    // Start is called before the first frame update
    void Start()
    {
        lvControl = lvObject.GetComponent<LevelController>();
        if(singlePlay!=null)
        MenuSnakeAni = GameObject.Find("MenuSnakeAni").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
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
