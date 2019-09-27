using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogController : MonoBehaviour
{

    public List<Dialogs> dialogs;
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public TextMeshProUGUI text;

    private GameObject dialogPanel;

    public int currentDialog;
    public int currentIndex;



    // Start is called before the first frame update
    void Start()
    {
        dialogs = new List<Dialogs>();
        AddDialog();

        nameText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        iconImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        text = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

        dialogPanel = transform.GetChild(0).gameObject;

        //DisplayDialog(0, 0);
    }

    public void DisplayDialog(int dialog, int index)
    {
        nameText.text = dialogs[dialog].speakerName[index];
        iconImage.sprite = Resources.Load<Sprite>("Sprites/DialogSprites/" + dialogs[dialog].iconSpriteName[index]);
        text.text = dialogs[dialog].text[index];
        dialogs[dialog].haveReadThis[index] = true;
    }

    public void StartNewDialog(int dialogNumber)
    {
        if (!dialogs[dialogNumber].haveRead)
        {
            dialogPanel.SetActive(true);
            Time.timeScale = 0;
            currentDialog = dialogNumber;
            currentIndex = 0;
            DisplayDialog(dialogNumber, 0);
        }
    }

    public void FinishDialog()
    {
        dialogPanel.SetActive(false);
        dialogs[currentDialog].haveRead = true;
        Time.timeScale = 1;
    }

    public void AddDialog()
    {
        // haveRead;
        // List:  haveReadThis;
        // List:  speakerName;
        // List:  iconSpriteName;
        // List:  text;

        // ++++++ Make sure ALL List have same COUNT ++++++

        // dialog 1
        dialogs.Add(new Dialogs(
                false,
                new List<bool> { false, false, false, false },
                new List<string> { "Wolf", "You", "Wolf", "You" },
                new List<string> { "NormalWolf", "Snake", "NormalWolf", "Snake" },
                new List<string> { "Awoo~~~~~", "Howl!!!!!", "Car~son", "Carson!!!!!!" }));
        // dialog 2

    }

    public void NextSentence()
    {
        if (currentIndex < dialogs[currentDialog].speakerName.Count - 1)
        {
            currentIndex++;
            DisplayDialog(currentDialog, currentIndex);
        }
        else
        {
            FinishDialog();
        }
    }




    // Update is called once per frame
    void Update()
    {

    }
}
