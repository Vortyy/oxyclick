using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenShop : MonoBehaviour
{
    private OxygenScore oxyScoreUI;
    [SerializeField] GameObject itemActions;
    [SerializeField] TextMeshProUGUI itemDesc;
    [SerializeField] TextMeshProUGUI buyCostText;
    [SerializeField] TextMeshProUGUI upgradeCostText;

    public bool level5Capped = true;
    public bool level10Capped = true;
    private int toggled = 0;

    private string itemName;
    private float value;
    private int quantity;
    private int level;
    private float buyCost;
    private float upgradeCost;
    private float multiplier;


    [SerializeField] Button basilButton;
    private const string basilName = "Basil";
    private bool basilLocked = true;
    private const float basilValue = 0.001f;
    private int basilQuantity = 0;
    private int basilLevel = 1;
    private float basilMod;

    private void Start()
    {
        oxyScoreUI = GameObject.FindObjectOfType<OxygenScore>();
        basilButton.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (oxyScoreUI.oxyScore >= basilValue * 10f)
        {
            basilButton.GetComponent<Button>().interactable = true;
        }
    }

    public void LoadBasil()
    {
        itemName = basilName;
        value = basilValue;
        quantity = basilQuantity;
        level = basilLevel;
        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);

        toggled = 1;
        UpdateItem();
    }

    private void UpdateBasil()
    {
        basilQuantity = quantity;
        basilLevel = level;
        basilMod = quantity * (basilValue + (basilValue * multiplier));
    }

    // Called whenever one of the item's values is chnaged
    public void UpdateItem()
    {
        if (value < 1f)
        {
            itemDesc.text = "Selected: " + itemName + "\nCount: " + quantity.ToString() + "\nLevel: " + level.ToString() + "\nProduction: " + (1000 * quantity * (value + (value * multiplier))).ToString("0.#") + " g/s";
        }
        else
        {
            itemDesc.text = "Selected: " + itemName + "\nCount: " + quantity.ToString() + "\nLevel: " + level.ToString() + "\nProduction: " + (quantity * (value + (value * multiplier))).ToString("0.0#") + " kg/s";
        }
        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);
        UpdateMultiplier();

        if (buyCost < 1f)
        {
            buyCostText.text = "Buy: " + (buyCost * 1000f).ToString("0") + " g";
        }
        else
        {
            buyCostText.text = "Buy: " + buyCost.ToString("0") + " kg";
        }
         
        if (upgradeCost < 1f)
        {
            upgradeCostText.text = "Upgrade: " + (upgradeCost * 1000f).ToString("0") + " g";
        }
        else
        {
            upgradeCostText.text = "Upgrade: " + upgradeCost.ToString("0") + " kg";
        }

        UpdateToggledItem();
        UpdateModifiers();
    }

    // Buy toggled item
    public void BuyItem()
    {
        if (oxyScoreUI.oxyScore >= buyCost)
        {
            oxyScoreUI.RemoveToScore(buyCost);
            quantity++;
        }

        UpdateItem();
        UpdateModifiers();
    }

    // Upgrade toggled item
    public void UpgradeItem()
    {
        if (!level10Capped)
        {
            if (level5Capped && level < 5)
            {
                oxyScoreUI.RemoveToScore(upgradeCost);
                level++;
                UpdateMultiplier();

                UpdateItem();
                UpdateModifiers();
            }
            else if (level10Capped && level < 9)
            {
                oxyScoreUI.RemoveToScore(upgradeCost);
                level++;
                UpdateMultiplier();

                UpdateItem();
                UpdateModifiers();
            }
        }        
    }

    // Manual input when the player clicks
    public void ManualTap()
    {
        oxyScoreUI.AddToScore(0.001f);
    }

    public void UpdateModifiers()
    {
        oxyScoreUI.modifiers = basilMod;
    }

    private void UpdateToggledItem()
    {
        switch (toggled)
        {
            case 1:
                UpdateBasil();
                break;
        }
    }

    private void UpdateMultiplier()
    {
        switch(level)
        {
            case 1: // Default
                multiplier = 0;
                break;
            case 2: // +25%
                multiplier = 0.25f;
                break;
            case 3: // +50%
                multiplier = 0.50f;
                break;
            case 4: // +75%
                multiplier = 0.75f;
                break;
            case 5: // +100%
                multiplier = 1.00f;
                break;
            case 6: // +120%
                multiplier = 1.20f;
                break;
            case 7: // +140%
                multiplier = 1.40f;
                break;
            case 8: // +160%
                multiplier = 1.60f;
                break;
            case 9: // +180%
                multiplier = 1.80f;
                break;
            case 10: // +200%
                multiplier = 2.00f;
                break;
        }
    }
}
