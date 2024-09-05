using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDice : MonoBehaviour
{
    [SerializeField] bool isWhiteDice;

    public void ApplyDice()
    {
        if (isWhiteDice)
        {
            ResourceManager.Instance.whiteDice++;
        }
        else
        {
            ResourceManager.Instance.redDice++;
        }
        ResourceManager.Instance.UpdateGui();
    }
}
