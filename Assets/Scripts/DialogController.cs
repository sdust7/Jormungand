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

        DisplayDialog(0, 0);
    }

    public void DisplayDialog(int dialogIndex, int currentDialog)
    {
        nameText.text = dialogs[dialogIndex].speakerName[currentDialog];
        iconImage.sprite = Resources.Load<Sprite>("Sprites/DialogSprites/" + dialogs[dialogIndex].iconSpriteName[currentDialog]);
        text.text = dialogs[dialogIndex].text[currentDialog];
    }

    public void StartDialog(int dialogNumber) {
        dialogPanel.SetActive(true);
        Time.timeScale = 0;
        currentDialog = dialogNumber;
        currentIndex = 0;
        DisplayDialog(dialogNumber, 0);
    }

    public void AddDialog()
    {
        // index 1
        dialogs.Add(new Dialogs(false,
            new List<bool> { false, false },
            new List<string> { "Wolf", "You", "Wolf", "You" },
            new List<string> { "NormalWolf", "Snake", "NormalWolf", "Snake" },
            new List<string> { "Awoo~~~~~", "Howl!!!!!", "Car~son", "Carson!!!!!!" }));
        // index 2

    }

    public void NextButton()
    {
        currentIndex++;
        DisplayDialog(currentDialog, currentIndex);

    }




    // Update is called once per frame
    void Update()
    {

    }
}
