using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private LevelController lvControl;
    private GameObject gameOver;
    private GameObject gameWin;
    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        gameOver=transform.GetChild(0).gameObject;
        gameWin=transform.GetChild(1).gameObject;
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
}
