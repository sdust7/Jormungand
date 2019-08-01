using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    private GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("DemoLevel");

    }

    public void GameOver()
    {
        restartButton.SetActive(true);
    }
}
