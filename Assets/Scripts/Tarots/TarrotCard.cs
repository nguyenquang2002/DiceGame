using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TarotEffect
{
    ReverseNextRoll,
    DoubleNextRoll,
    RandomResource,
    IncreaseTileLevel,
    DecreaseTileLevel,
    TeleportToStart
}

public class TarrotCard : MonoBehaviour
{
    public TarotEffect effect;
    [SerializeField] GameObject frontCard, backCard;
    public bool isChosen = false;
    public static TarrotCard selectedCard;

    private void Awake()
    {
        frontCard = transform.GetChild(0).gameObject;
        backCard = transform.GetChild(1).gameObject;
        frontCard.SetActive(false);
        backCard.SetActive(true);
    }

    public void OnCardClick()
    {
        if (selectedCard != null || isChosen)
            return;

        selectedCard = this;
        isChosen = true;
        frontCard.SetActive(true);
        backCard.SetActive(false);
        TarrotManager.Instance.DisplayOKButton();
    }
    public void ApplyEffect()
    {
        if(!isChosen) return;
        switch (effect)
        {
            case TarotEffect.ReverseNextRoll:
                CharacterMovement.Instance.isReverse = true;
                CharacterMovement.Instance.CheckIndleOrStunning();
                break;
            case TarotEffect.DoubleNextRoll:
                DiceController.Instance.isDoubleNextRoll = true;
                break;
            case TarotEffect.RandomResource:
                CharacterMovement.Instance.TriggerRandomResourceTile();
                break;
            case TarotEffect.IncreaseTileLevel:
                CharacterMovement.Instance.ChangeTileLevel(true);
                break;
            case TarotEffect.DecreaseTileLevel:
                CharacterMovement.Instance.ChangeTileLevel(false);
                break;
            case TarotEffect.TeleportToStart:
                CharacterMovement.Instance.TeleportToStart();
                break;
        }
        selectedCard = null;
    }
}
