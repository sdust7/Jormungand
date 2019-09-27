using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogs : MonoBehaviour
{
    public bool haveRead;
    public List<bool> haveReadThis;
    public List<string> speakerName;
    public List<string> iconSpriteName;
    public List<string> text;


    public Dialogs(bool haveRead ,List<bool> haveReadThis, List<string> speakerName, List<string> iconSpriteName, List<string> text)
    {
        this.haveRead = haveRead;
        this.haveReadThis = haveReadThis;
        this.speakerName = speakerName;
        this.iconSpriteName = iconSpriteName;
        this.text = text;

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
