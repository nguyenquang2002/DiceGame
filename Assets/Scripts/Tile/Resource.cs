using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] string resourceType;
    [SerializeField] bool isSpecial = false;
    [SerializeField] int amount, level = 1, maxAmountSpecial;
    [SerializeField] TextMeshProUGUI amountText, levelText;
    [SerializeField] EffectTile effectPrefab;

    private void Start()
    {
        UpdateGUI(level, FinalAmount());
    }

    public void ReceiveResource()
    {
        ResourceManager.Instance.AddResource(resourceType, FinalAmount());
        if(level < 3)
        {
            level++;
        }
        UpdateGUI(level, FinalAmount());
    }

    public EffectTile CreateEffect()
    {
        return Instantiate(effectPrefab, transform.GetChild(0));
    }

    public void ChangeLevel(bool isIncrese)
    {
        if (isIncrese)
        {
            if(level < 3)
            {
                level++;
            }
            EffectTile effect = CreateEffect();
            effect.isResource = false;
            effect.isLevelUp = true;
            effect.Effect(0.2f, 0.8f, 1f);
        }
        else
        {
            if(level > 1)
            {
                level--;
            }
            EffectTile effect = CreateEffect();
            effect.isResource = false;
            effect.isLevelUp = false;
            effect.Effect(0.8f, 0.2f, 1f);
        }
        UpdateGUI(level, FinalAmount());
    }

    public int FinalAmount()
    {
        int finalAmount = amount * level;
        if (isSpecial && level == 3)
        {
            finalAmount = maxAmountSpecial;
        }
        return finalAmount;
    }

    private void UpdateGUI(int finalLevel, int finalAmount)
    {
        levelText.text = "Lv." + finalLevel;
        if(finalAmount < 10000)
        {
            amountText.text = finalAmount.ToString();
        }
        else
        {
            int temp = finalAmount / 1000;
            amountText.text = temp.ToString() + "K";
        }
    }
}
