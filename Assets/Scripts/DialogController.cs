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

    private GameObject snake;

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
        snake = GameObject.Find("Head");
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
        //    new List<bool> {false },
        //    new List<string> {"" },
        //    new List<string> {"" },
        //    new List<string> {"" }));


        // ++++++ Make sure ALL List have same COUNT ++++++

        // dialog 0
        dialogs.Add(new Dialogs(
                false,
                new List<bool> { false, false, false, false, false, false, false, false, false, false, false },
                new List<string> { "Thin Wolf", "Jormungand", "Thin Wolf", "Jormungand", "Thin Wolf", "Jormungand", "Thin Wolf", "Jormungand", "Thin Wolf", "Jormungand", "Jormungand" },
                new List<string> { "NormalWolf", "SnakeIcon", "NormalWolf", "SnakeIcon", "NormalWolf", "SnakeIcon", "NormalWolf", "SnakeIcon", "NormalWolf", "SnakeIcon", "SnakeIcon" },
                new List<string> { "Hey! I know you! Although you looks short, you are brother of Fenrir!", "Damn Odin... He cut my body off!!", "I see... also you looks so cute", "He cursed me and I can't getting bigger with that curse!",
                                               "That's so poor...", "You are right... wait... Why you eating apple?? YOU ARE A WOLF!!!", "All sheep in forest became so clever since Fenrir has been sealed. I believe Odin did something. So we hardly to catch sheep... And apple is the only food we can get in this forest... Well it still taste good.",
                                               "... OK... I'm so hungry, please share me an apple.", "Ok, you can take 2 apples beside me. But don't take all 3 apples!", "Don't worry. Trust me", "(Because Odin's curse I can't stop moving. But press A and D can change the direction I moving to. Let get that 2 apples.)"}));
        // dialog 1
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false, false },
            new List<string> { "Thin Wolf", "Jormungand", "Thin Wolf" },
            new List<string> { "NormalWolf", "SnakeIcon", "NormalWolf" },
            new List<string> { "Wait! You got longer???", "!! Why? Wait... Odin said I can't getting \"Bigger\" but he didn't said \"Longer\" !", "That's sounds so inadequate..." }));
        // dialog 2
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false },
            new List<string> { "Thin Wolf", "Jormungand" },
            new List<string> { "NormalWolf", "SnakeIcon" },
            new List<string> { "Fenrir is in the south of the forest, He may needs your help.", "Ok, I will go and see what I can do for him." }));
        // dialog 3
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false, false, false },
            new List<string> { "Thin Wolf", "Thin Wolf", "Jormungand", "Jormungand" },
            new List<string> { "NormalWolf", "NormalWolf", "SnakeIcon", "SnakeIcon" },
            new List<string> { "WHAT ARE YOU DOING!! That's my food for next month! Oh my Fenrir... Oh...", "(Dead because so angry)", "......\nI'm sorry, dude......", "He is dead...  It's so wastefulI if I just let his body here to decompose..." }));
        // dialog 4
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false, false },
            new List<string> { "Jormungand", "Jormungand", "Jormungand" },
            new List<string> { "SnakeIcon", "SnakeIcon", "SnakeIconSmile" },
            new List<string> { "nom~nom~(chewing)", "......", "D E L I C I O U S ! ! !" }));
        // dialog 5
        dialogs.Add(new Dialogs(
            false,
            new List<bool> { false, false, false, false, false },
            new List<string> { "AxeWolf", "Jormungand", "AxeWolf", "Jormungand", "AxeWolf" },
            new List<string> { "AxeWolf", "SnakeIcon", "AxeWolf", "SnakeIcon", "AxeWolf" },
            new List<string> {"Yo, what's up little bro?",
                              "I'm feeling dizzy, the apples have been eaten up and sheep run too fast to catch!!",
                              "I've got many apples, I can share that to you if you hlep me to cut the tree~~~",
                              "Yeah! for sure!!",
                              "So you could learn how to help me cut the tree right now. Take my axe, then cut the tree and collect the trunk, it's in the middle of the tree, "
            }));
    }

    public void NextSentence()
    {
        SpecialActions();
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

    public void SpecialActions()
    {
        // Current index should be 1 less than the sentence you want the actions happen.
        switch (currentDialog)
        {
            case 3: // Thin wolf dead
                switch (currentIndex)
                {
                    case 0: // Thin wolf dead
                        GameObject.Find("ThinWolf").GetComponent<SpeakerController>().ThinWolfDead();
                        break;
                }
                break;
            case 4:// Eating Thin wolf
                switch (currentIndex)
                {
                    case 1: // Finished eating
                        GameObject.Find("ThinWolf").SetActive(false);
                        break;
                }
                break;
            case 5: //Axe wolf
                switch (currentIndex)
                {
                    case 4:
                        snake.GetComponent<SnakeController>().AddEquipment(Equipments.Axe);
                        GameObject.Find("AxeWolf").transform.GetChild(0).gameObject.SetActive(true);
                        break;
                }
                break;



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
