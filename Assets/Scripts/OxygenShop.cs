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
    [SerializeField] TextMeshProUGUI buyCost;
    [SerializeField] TextMeshProUGUI upgradeCost;


    public bool level5Capped = true;
    public bool level10Capped = true;
    private float score = 0;
    private int toggled = 0;

    private float sproutNb = 0;
    private float sproutValue = 0.001f;
    private float sproutMultiplier = 0f;
    private int sproutLevel = 1;
    private float sproutCost = 0;
    private float sproutUpgradeCost = 0;


    private float bonzaiNb = 0;
    private float bonzaiValue = 0.01f;
    private float bonzaiMultiplier = 0f;
    private int bonzaiLevel = 1;
    private float bonzaiCost = 0;
    private float bonzaiUpgradeCost = 0;

    private float cmplantNb = 0;
    private float cmplantValue = 0.1f;
    private float cmplantMultiplier = 0f;
    private int cmplantLevel = 1;

    private float treeNb = 0;
    private float treeValue = 1f;
    private float treeMultiplier = 0f;
    private int treeLevel = 1;

    private float greenhouseNb = 0;
    private float greenhouseValue = 10f;
    private float greenhouseMultiplier = 0f;
    private int greenhouseLevel = 1;

    private float recyclerNb = 0;
    private float recyclerValue = 100f;
    private float recyclerMultiplier = 0f;
    private int recyclerLevel = 1;

    private float factoryNb = 0;
    private float factoryValue = 1000f;
    private float factoryMultiplier = 0f;
    private int factoryLevel = 1;

    private void Start()
    {
        oxyScoreUI = GameObject.FindObjectOfType<OxygenScore>();
    }

    private void ToggledCheck(int val)
    {
        if (val != 0)
        {
            toggled = val;
            itemActions.gameObject.SetActive(true);
        }
    }

    public void OpenSprout()
    {
        ToggledCheck(1);
        itemDesc.text = "Selected: Sprout\nCount: " + sproutNb.ToString() + "\nLevel: " + sproutLevel.ToString() + "\nProduction: " + (1000 * sproutNb * (sproutValue + (sproutValue * sproutMultiplier))).ToString() + " g/s";
        sproutCost = (sproutValue * 10f) + (sproutValue * 5f * sproutNb);
        sproutUpgradeCost = (sproutLevel * 2.5f) * (sproutValue * 100f);

        if (sproutCost < 1f)
        {
            buyCost.text = "Buy: " + (sproutCost * 1000f).ToString("0") + " g";
        }
        else
        {
            buyCost.text = "Buy: " + sproutCost.ToString("0.00") + " kg";
        }
         
        if (sproutUpgradeCost < 1f)
        {
            upgradeCost.text = "Upgrade: " + (sproutUpgradeCost * 1000f).ToString("0") + " g";
        }
        else
        {
            upgradeCost.text = "Upgrade: " + sproutUpgradeCost.ToString("0") + " kg";
        }
    }

    public void OpenBonzai()
    {
        ToggledCheck(2);
        itemDesc.text = "Selected: Bonzai\nCount: " + bonzaiNb.ToString() + "\nLevel: " + bonzaiLevel.ToString() + "\nProduction: " + (1000 * bonzaiNb * (bonzaiValue + (bonzaiValue * bonzaiMultiplier))).ToString() + " g/s";
        bonzaiCost = (bonzaiValue * 10f) + (bonzaiValue * 5f * bonzaiNb);
        if (bonzaiCost < 1f)
        {
            buyCost.text = "Buy: " + (bonzaiCost * 1000f).ToString("0") + " g";
        }
        else
        {
            buyCost.text = "Buy: " + bonzaiCost.ToString("0.00") + " kg";
        }
    }

    public void Buy()
    {
        switch (toggled)
        {
            case 1:
                if (oxyScoreUI.oxyScore >= sproutCost)
                {
                    oxyScoreUI.RemoveToScore(sproutCost);
                    sproutNb++;
                    OpenSprout();
                }
                break;
            case 2:
                if (oxyScoreUI.oxyScore >= bonzaiCost)
                {
                    oxyScoreUI.RemoveToScore(bonzaiCost);
                    bonzaiNb++;
                    OpenBonzai();
                }
                break;
        }

        UpdateModifiers();
    }

    public void SproutTapScoreUpdate()
    {
        oxyScoreUI.AddToScore(0.001f);
    }


    private void UpdateModifiers()
    {
        oxyScoreUI.modifiers = (sproutNb * (sproutValue + (sproutValue * sproutMultiplier))) + (bonzaiNb * (bonzaiValue + (bonzaiValue * bonzaiMultiplier))) + (cmplantNb * (cmplantValue + (cmplantValue * cmplantMultiplier))) + (treeNb * (treeValue + (treeValue * treeMultiplier))) + (greenhouseNb * (greenhouseValue + (greenhouseValue * greenhouseMultiplier))) + (recyclerNb * (recyclerValue + (recyclerValue * recyclerMultiplier))) + (factoryNb * (factoryValue + (factoryValue * factoryMultiplier)));
    }

    private void UpdateMultiplier(float multiplier, int level)
    {
        switch(level)
        {
            case 1: // Default
                multiplier = 0;
                break;
            case 2: // +10%
                multiplier = 0.10f;
                break;
            case 3: // +15%
                multiplier = 0.15f;
                break;
            case 4: // +25%
                multiplier = 0.25f;
                break;
            case 5: // +50%
                multiplier = 0.50f;
                break;
            case 6: // +100%
                multiplier = 1.00f;
                break;
            case 7: // +150%
                multiplier = 1.50f;
                break;
            case 8: // +200%
                multiplier = 2.00f;
                break;
            case 9: // +250%
                multiplier = 2.50f;
                break;
            case 10: // +400%
                multiplier = 5.00f;
                break;

        }
    }
}
