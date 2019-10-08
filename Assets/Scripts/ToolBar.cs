using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolBar : MonoBehaviour
{
    private enum SpriteOrder
    {
        Empty, HealthPotion, EnergyPotion, AppleCore, Wood, SheepBone
    }

    public enum ItemsList
    {
        Empty = 0,
        EnergyPotion = 1,
        HealthPotion = 2,

        AppleCore = 7,
        SheepBone = 8,
        Wood = 9,
    }

    private Image[] slots;
    private TextMeshProUGUI[] counts;
    private Items[] allItem;
    private Items[] allUsable;
    public Sprite[] itemSprites;


    // Start is called before the first frame update
    void Start()
    {
        slots = new Image[11];
        counts = new TextMeshProUGUI[11];
        Items empty = new Items();
        allItem = new Items[] { empty, empty, empty, empty, empty };
        allUsable = new Items[] { empty, empty, empty, empty, empty, empty };
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = transform.GetChild(i).GetComponent<Image>();
            counts[i] = transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        UpdateUI();
    }
    
    public void DebugGotItemSlot1()
    {
        GotItem(new Items("EnergyPotion", true, 1));
    }

    public void DebugGotItemSlot2()
    {
        GotItem(new Items("HealthPotion", true, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GotItem(Items item)
    {
        if (!HasItem(item))
        {
            if (item.usable == false)
            {
                for (int i = 0; i < allItem.Length; i++)
                {
                    if (allItem[i].name == "Empty")
                    {
                        allItem[i] = item;
                        UpdateUI();
                        return true;
                    }
                }
                return false;
            }
            else
            {
                for (int i = 0; i < allUsable.Length; i++)
                {
                    if (allUsable[i].name == "Empty")
                    {
                        allUsable[i] = item;
                        UpdateUI();
                        return true;
                    }
                }
                return false;
            }
        }
        else
        {
            UpdateUI();
            return true;
        }
    }

    private bool HasItem(Items item)
    {
        if (item.usable == false)
        {
            for (int i = 0; i < allItem.Length; i++)
            {
                if (allItem[i].name == item.name)
                {
                    allItem[i].count++;
                    print(allItem[i].count);

                    return true;
                }
            }
            return false;
        }
        else
        {

            for (int i = 0; i < allUsable.Length; i++)
            {
                if (allUsable[i].name == item.name)
                {
                    allUsable[i].count++;
                    return true;
                }
            }

            return false;
        }
    }

    private void UpdateUI()
    {

        for (int i = 0; i < allUsable.Length; i++)
        {
            switch (allUsable[i].name)
            {
                case "Empty":
                    slots[i].sprite = itemSprites[(int)SpriteOrder.Empty];
                    counts[i].enabled = false;

                    break;
                case "HealthPotion":
                    slots[i].sprite = itemSprites[(int)SpriteOrder.HealthPotion];
                    counts[i].enabled = true;
                    counts[i].text = "x" + allUsable[i].count.ToString();
                    break;
                case "EnergyPotion":
                    slots[i].sprite = itemSprites[(int)SpriteOrder.EnergyPotion];
                    counts[i].enabled = true;
                    counts[i].text = "x" + allUsable[i].count.ToString();
                    break;
                default:
                    break;
            }
        }

        for (int i = 0; i < allItem.Length; i++)
        {
            switch (allItem[i].name)
            {
                case "Empty":
                    slots[i + 6].sprite = itemSprites[(int)SpriteOrder.Empty];
                    counts[i + 6].enabled = false;
                    break;
                case "AppleCore":
                    slots[i + 6].sprite = itemSprites[(int)SpriteOrder.AppleCore];
                    counts[i + 6].enabled = true;
                    counts[i + 6].text = "x" + allItem[i].count.ToString();
                    break;
                case "Wood":
                    slots[i + 6].sprite = itemSprites[(int)SpriteOrder.Wood];
                    counts[i + 6].enabled = true;
                    counts[i + 6].text = "x" + allItem[i].count.ToString();
                    break;
                case "SheepBone":
                    slots[i + 6].sprite = itemSprites[(int)SpriteOrder.SheepBone];
                    counts[i + 6].enabled = true;
                    counts[i + 6].text = "x" + allItem[i].count.ToString();
                    break;
                default:
                    break;
            }
        }
    }


}
