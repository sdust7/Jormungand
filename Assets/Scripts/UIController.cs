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
    private Transform weaponPanel;
    private List<string> equipmensName;
    public List<Sprite> equipmentUI;
    // Start is called before the first frame update
    void Start()
    {

        //for (int i = 0; i < 2; i++)
        //{
        //    equipmensName[i] = new Equipments(i);
        //}

        switch (transform.name)
        {
            case "Canvas":
                lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
                gameOver = transform.GetChild(0).gameObject;
                gameWin = transform.GetChild(1).gameObject;
                break;
            case "UIPanel":
                equipmensName = new List<string>() { "Empty", "AXE", "FireworkStand" };
                woodCount = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
                weaponPanel = transform.GetChild(4);
                UpdateWeaponUI(equipmensName[0], equipmensName[0],equipmensName[0]);
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

    public void WeaponChanged(List<Transform> equipments, int current)
    {
        string rightName;
        string leftName;
        string currentName = equipments[current].name;
        if (equipments.Count > 1)
        {
            if (current == equipments.Count - 1)
            {
                rightName = equipments[0].name;
                leftName = equipments[current - 1].name;
            }
            else if (current == 0)
            {
                rightName = equipments[current + 1].name;
                leftName = equipments[equipments.Count - 1].name;
            }
            else
            {
                leftName = equipments[current - 1].name;
                rightName = equipments[current + 1].name;
            }
            UpdateWeaponUI(leftName,rightName,currentName);

        }

    }

    public void UpdateWeaponUI(string leftName,string rightName,string currentName)
    {
        for (int i = 0; i < equipmensName.Count; i++)
        {
            if (leftName == equipmensName[i])
            {
                weaponPanel.GetChild(0).GetComponent<Image>().sprite = equipmentUI[i];
            }
            if (rightName == equipmensName[i])
            {
                weaponPanel.GetChild(2).GetComponent<Image>().sprite = equipmentUI[i];
            }
            if (currentName == equipmensName[i])
            {
                weaponPanel.GetChild(1).GetComponent<Image>().sprite = equipmentUI[i];

            }
        }
    }
}
