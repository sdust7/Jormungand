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




    // Start is called before the first frame update
    void Start()
    {
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


    public void ClickSingle()
    {
        SceneManager.LoadScene("SampleScene");
    }



}
