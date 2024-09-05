using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool stun;
    
    public void ApplyEffect()
    {
        if (stun)
        {
            CharacterMovement.Instance.isStunning = true;
        }
        else
        {
            TriggerTarrot();
        }
    }

    private void TriggerTarrot()
    {
        TarrotManager.Instance.DisplayRandomTarotCards();
    }
}
