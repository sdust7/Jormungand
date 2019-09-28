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

    private bool dialogInProgress;

    // Start is called before the first frame update
    void Start()
    {
        dialogs = new List<Dialogs>();
        AddDialog();

        nameText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        iconImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        text = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

        dialogPanel = transform.GetChild(0).gameObject;
        dialogInProgress = false;
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
            dialogInProgress = true;
        }
    }

    public void FinishDialog()
    {
        dialogPanel.SetActive(false);
        dialogs[currentDialog].haveRead = true;
        Time.timeScale = 1;
        dialogInProgress = false;
    }

    public void AddDialog()
    {
        // haveRead;
        // List:  haveReadThis;
        // List:  speakerName;
        // List:  iconSpriteName;
        // List:  text;

        // Copy this 

        // dialog n
        //dialogs.Add(new Dialogs(
        //    false,
        //    new List<bool> { },
        //    new List<string> { },
        //    new List<string> { },
        //    new List<string> { } ));


        // ++++++ Make sure ALL List have same COUNT ++++++

        // dialog 0
        dialogs.Add(new Dialogs(
                false,
                new List<bool> { false, false, false, false, false, false, false, false, false, false, false },
                new List<string> { "Thin Wolf", "You", "Thin Wolf", "You", "Thin Wolf", "You", "Thin Wolf", "You", "Thin Wolf", "You", "You" },
                new List<string> { "NormalWolf", "Snake", "NormalWolf", "Snake", "NormalWolf", "Snake", "NormalWolf", "Snake", "NormalWolf", "Snake", "Snake" },
                new List<string> { "Hey! I know you! Although you looks short, you are brother of Fenrir!", "Damn Odin... He cut my body off!!", "I see... also you looks so cute", "He cursed me and I can't getting bigger with that curse!",
                                               "That's so poor...", "You are right... wait... Why you eating apple?? YOU ARE A WOLF!!!", "All sheep in forest became so clever since Fenrir has been sealed. I believe Odin did something. So we hardly to catch sheep... And apple is the only food we can get in this forest... Well it still taste good.",
                                               "... OK... I'm so hungry, please share me an apple.", "Ok, you can take 2 apples beside me. But don't take all 3 apples!", "Don't worry. Trust me", "(Because Odin's curse I can't stop moving. But press A and D can change the direction I moving to. Let get that 2 apples.)"}));
        // dialog 1
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false, false },
            new List<string> { "Thin Wolf", "You", "Thin Wolf" },
            new List<string> { "NormalWolf", "Snake", "NormalWolf" },
            new List<string> { "Wait! You got longer???", "!! Why? Wait... Odin said I can't getting \"Bigger\" but he didn't said \"Longer\" !", "That's sounds so inadequate..." }));
        // dialog 2
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false },
            new List<string> { "Thin Wolf", "You" },
            new List<string> { "NormalWolf", "Snake" },
            new List<string> { "Fenrir is in the south of the forest, He may needs your help.", "Ok, I will go and see what I can do for him." }));
        // dialog 3
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false },
            new List<string> { "Thin Wolf" },
            new List<string> { "NormalWolf" },
            new List<string> { "WHAT ARE YOU DOING!! That's my food for next month! Oh my Fenrir... Oh... (Dead because so angry)" }));

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
        if (dialogInProgress)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextSentence();
            }
        }

    }
}
