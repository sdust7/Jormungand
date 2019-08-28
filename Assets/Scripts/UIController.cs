using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private LevelController lvControl;
    private GameObject gameOver;
    private GameObject gameWin;
    private TextMeshProUGUI woodCount;
    // Start is called before the first frame update
    void Start()
    {
        switch (transform.name)
        {
            case "Canvas":
                lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
                gameOver = transform.GetChild(0).gameObject;
                gameWin = transform.GetChild(1).gameObject;
                break;
            case "UIPanel":
                woodCount = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    public void GameWin()
    {
        gameWin.SetActive(true);
    }
    public void UpdateWood(int number)
    {
        woodCount.text = number.ToString();
    }
}
