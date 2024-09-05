using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    public static DiceUI Instance;

    [SerializeField] GameObject dice, diceRoll, diceResult;
    [SerializeField] SkeletonAnimation diceAnimation;
    [SerializeField] Image diceNumber;
    [SerializeField] List<Sprite> whiteSprite, redSprite;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        diceAnimation = diceRoll.GetComponentInChildren<SkeletonAnimation>();
        diceNumber = diceResult.GetComponentInChildren<Image>();
    }
    
    public IEnumerator OnDiceClickCoroutine(bool isWhite, float delay)
    {
        dice.SetActive(false);
        diceResult.SetActive(false);
        diceRoll.SetActive(true);
        if(diceAnimation != null)
        {
            if (isWhite)
            {
                diceAnimation.AnimationState.SetAnimation(0, "GreenRoll", false);
            }
            else
            {
                diceAnimation.AnimationState.SetAnimation(0, "RedRoll", false);
            }
        }
        yield return new WaitForSeconds(delay);
        diceRoll.SetActive(false);
        diceResult.SetActive(true);
    }
    public void SetImageDice(int result, bool isWhite)
    {
        if(diceNumber != null)
        {
            if (isWhite)
            {
                diceNumber.sprite = whiteSprite[result - 1];
            }
            else
            {
                diceNumber.sprite = redSprite[result - 1];
            }
        }
    }
    public void DisplayDice()
    {
        dice.SetActive(true);
        diceResult.SetActive(false);
        diceRoll.SetActive(false);
    }
}
