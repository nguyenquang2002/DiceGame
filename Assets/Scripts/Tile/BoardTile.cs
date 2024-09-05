using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardTile : MonoBehaviour
{
    private Image img;

    private void Start()
    {
        img = GetComponentInChildren<Image>();
    }
    public void TriggerEffect()
    {
        string tempText = "";
        if(GetComponent<Resource>() != null)
        {
            tempText = GetComponent<Resource>().FinalAmount().ToString();
            GetComponent<Resource>().ReceiveResource();
        }
        else if(GetComponent<AddDice>() != null)
        {
            GetComponent<AddDice>().ApplyDice();
            tempText = "1";
        }
        else if(GetComponent<Trap>() != null)
        {
            GetComponent<Trap>().ApplyEffect();
        }
        ResourceDisplay.Instance.UpdateGUI(img, tempText);
        ResourceDisplay.Instance.DisplayEffect();
    }
}
