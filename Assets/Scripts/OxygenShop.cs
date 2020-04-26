using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenShop : MonoBehaviour
{
    OxygenScore oxyScoreUI;
    public bool level5Capped = true;
    public bool level10Capped = true;
    private float score = 0;

    public float sproutNb = 0;
    public float sproutValue = 0.001f;
    public float sproutMultiplier = 0f;
    public int sproutLevel = 1;

    public float bonzaiNb = 0;
    public float bonzaiValue = 0.01f;
    public float bonzaiMultiplier = 0f;
    public int bonzaiLevel = 1;

    public float cmplantNb = 0;
    public float cmplantValue = 0.1f;
    public float cmplantMultiplier = 0f;
    public int cmplantLevel = 1;

    public float treeNb = 0;
    public float treeValue = 1f;
    public float treeMultiplier = 0f;
    public int treeLevel = 1;

    public float greenhouseNb = 0;
    public float greenhouseValue = 10f;
    public float greenhouseMultiplier = 0f;
    public int greenhouseLevel = 1;

    public float recyclerNb = 0;
    public float recyclerValue = 100f;
    public float recyclerMultiplier = 0f;
    public int recyclerLevel = 1;

    public float factoryNb = 0;
    public float factoryValue = 1000f;
    public float factoryMultiplier = 0f;
    public int factoryLevel = 1;

    private void Start()
    {
        oxyScoreUI = GameObject.FindObjectOfType<OxygenScore>();
        UpdateModifiers();
    }

    private void Update()
    {
        
    }

    public void TapScoreUpdate()
    {
        oxyScoreUI.AddTapPoints(0.001f);
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
