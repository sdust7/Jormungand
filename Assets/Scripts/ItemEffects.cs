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




    public bool DoEffect(string name)
    {
        switch (name)
        {
            case "HealthPotion":
                return lvControl.RestoreSnakeHealth(20);
            case "EnergyPotion":
                return lvControl.RestoreSnakeEnergy(50);
        }
        return true;
    }
}
