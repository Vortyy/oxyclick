using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenShop : MonoBehaviour
{
    private OxygenScore oxyScoreUI;
    [SerializeField] GameObject itemActions;
    [SerializeField] TextMeshProUGUI itemDescText;
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
    private const string basilName = "Basil Sprout";
    private const float basilValue = 0.001f;
    private int basilQuantity = 0;
    private int basilLevel = 1;
    private float basilMod;

    [SerializeField] Button aloesButton;
    private const string aloesName = "Aloe Vera Sprout";
    private const float aloesValue = 0.01f;
    private int aloesQuantity = 0;
    private int aloesLevel = 1;
    private float aloesMod;

    [SerializeField] Button peepalButton;
    private const string peepalName = "Peepal Sprout";
    private const float peepalValue = 0.1f;
    private int peepalQuantity = 0;
    private int peepalLevel = 1;
    private float peepalMod;

    [SerializeField] Button plantButton;
    private const string plantName = "CMPlant";
    private const float plantValue = 1f;
    private int plantQuantity = 0;
    private int plantLevel = 1;
    private float plantMod;



    private void Start()
    {
        oxyScoreUI = GameObject.FindObjectOfType<OxygenScore>();
        itemActions.SetActive(false);

        // load buttons to their start settings
        basilButton.GetComponent<Button>().interactable = false;
        aloesButton.gameObject.SetActive(false);
        aloesButton.GetComponent<Button>().interactable = false;
        peepalButton.gameObject.SetActive(false);
        peepalButton.GetComponent<Button>().interactable = false;
        plantButton.gameObject.SetActive(false);
        plantButton.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (toggled != 0)
        {
            itemActions.SetActive(true);
        }

        if (oxyScoreUI.oxyScore >= plantValue * 10f)
        {
            plantButton.interactable = true;
        }
        else if (oxyScoreUI.oxyScore >= peepalValue * 10f)
        {
            peepalButton.interactable = true;
            plantButton.gameObject.SetActive(true);
        }
        else if (oxyScoreUI.oxyScore >= aloesValue * 10f)
        {
            aloesButton.interactable = true;
            peepalButton.gameObject.SetActive(true);
        }
        else if (oxyScoreUI.oxyScore >= basilValue * 10f)
        {
            basilButton.interactable = true;
            aloesButton.gameObject.SetActive(true);
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

    public void LoadAloes()
    {
        itemName = aloesName;
        value = aloesValue;
        quantity = aloesQuantity;
        level = aloesLevel;
        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);

        toggled = 2;
        UpdateItem();
    }

    public void LoadPeepal()
    {
        itemName = peepalName;
        value = peepalValue;
        quantity = peepalQuantity;
        level = peepalLevel;
        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);

        toggled = 3;
        UpdateItem();
    }

    public void LoadPlant()
    {
        itemName = plantName;
        value = plantValue;
        quantity = plantQuantity;
        level = plantLevel;
        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);

        toggled = 4;
        UpdateItem();
    }

    private void UpdateBasil()
    {
        basilQuantity = quantity;
        basilLevel = level;
        basilMod = quantity * (value + (value * multiplier));
    }

    private void UpdateAloes()
    {
        aloesQuantity = quantity;
        aloesLevel = level;
        aloesMod = quantity * (value + (value * multiplier));
    }

    private void UpdatePeepal()
    {
        peepalQuantity = quantity;
        peepalLevel = level;
        peepalMod = quantity * (value + (value * multiplier));
    }

    private void UpdatePlant()
    {
        plantQuantity = quantity;
        plantLevel = level;
        plantMod = quantity * (value + (value * multiplier));
    }

    // Called whenever one of the item's values is chnaged
    public void UpdateItem()
    {
        if (value < 1f)
        {
            itemDescText.text = "Selected: " + itemName + "\nCount: " + quantity.ToString() + "\nLevel: " + level.ToString() + "\nProduction: " + (1000 * quantity * (value + (value * multiplier))).ToString("0.#") + " g/s";
        }
        else
        {
            itemDescText.text = "Selected: " + itemName + "\nCount: " + quantity.ToString() + "\nLevel: " + level.ToString() + "\nProduction: " + (quantity * (value + (value * multiplier))).ToString("0.0#") + " kg/s";
        }

        buyCost = (value * 10f) + (value * 5f * quantity);
        upgradeCost = (level * 2.5f) * (value * 100f);
        UpdateMultiplier();

        if (buyCost < 1f)
        {
            buyCostText.text = "Buy 1: " + (buyCost * 1000f).ToString("0") + " g";
        }
        else
        {
            buyCostText.text = "Buy 1: " + buyCost.ToString("0") + " kg";
        }
         
        if (level5Capped && level == 5)
        {
            upgradeCostText.text = "Maxed";
        }
        else if (upgradeCost < 1f)
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
        if (level5Capped)
        {
            if (level < 5 && oxyScoreUI.oxyScore >= upgradeCost)
            {
                oxyScoreUI.RemoveToScore(upgradeCost);
                level++;
                UpdateMultiplier();

                UpdateItem();
                UpdateModifiers();
            }
        }
        else if (level < 10 && oxyScoreUI.oxyScore >= upgradeCost) { }
    }

    // Manual input when the player clicks
    public void ManualTap()
    {
        oxyScoreUI.AddToScore(0.001f);
    }

    public void UpdateModifiers()
    {
        oxyScoreUI.modifiers = basilMod + aloesMod + peepalMod + plantMod;
    }

    private void UpdateToggledItem()
    {
        switch (toggled)
        {
            case 1:
                UpdateBasil();
                break;
            case 2:
                UpdateAloes();
                break;
            case 3:
                UpdatePeepal();
                break;
            case 4:
                UpdatePlant();
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
