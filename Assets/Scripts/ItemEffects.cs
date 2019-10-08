using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    //private ToolBar toolBar;
    private LevelController lvControl;


    private void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        //toolBar = GameObject.Find("ToolBar").GetComponent<ToolBar>();
    }




    public void DoEffect(string name)
    {
        switch (name)
        {
            case "HealthPotion":
                lvControl.RestoreSnakeHealth(20);
                break;
            case "EnergyPotion":
                lvControl.RestoreSnakeEnergy(50);
                break;
        }

    }
}
