using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private static DiceController _instance;

    public static DiceController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DiceController>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("DiceController");
                    _instance = go.AddComponent<DiceController>();
                }
            }
            return _instance;
        }
    }

    private int diceResult;
    public bool canRoll = true;
    public bool isDoubleNextRoll = false;
    public int luckyDiceResult = 0;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void RollDice()
    {
        StartCoroutine(RollDiceCoroutine());
    }

    public IEnumerator RollDiceCoroutine()
    {
        if(ResourceManager.Instance.whiteDice > 0 && canRoll)
        {
            canRoll = false;
            diceResult = Random.Range(1, 7);
            DiceUI.Instance.SetImageDice(diceResult, true);
            yield return StartCoroutine(DiceUI.Instance.OnDiceClickCoroutine(true, 1f));
            ResourceManager.Instance.whiteDice--;
            if (isDoubleNextRoll)
            {
                diceResult *= 2;
            }
            ResourceManager.Instance.UpdateGui();
            //Debug.Log("Dice Result: " + diceResult);
            //Debug.Log("Dice left: " + ResourceManager.Instance.whiteDice);
            if (!CharacterMovement.Instance.isReverse && (!CharacterMovement.Instance.isStunning || diceResult % 2 == 0))
            {
                CharacterMovement.Instance.MovePlayer(diceResult);
                isDoubleNextRoll = false;
            }
            else
            {
                CharacterMovement.Instance.BackPlayer(diceResult);
            }
            
        }
    }

    public void LuckyDice()
    {
        StartCoroutine(LuckyDiceCoroutine());
    }

    public IEnumerator LuckyDiceCoroutine()
    {
        if (ResourceManager.Instance.redDice > 0 && canRoll && luckyDiceResult > 0)
        {
            canRoll = false;
            ResourceManager.Instance.redDice--;
            LuckyDiceUI.Instance.OnOKClick();
            DiceUI.Instance.SetImageDice(luckyDiceResult, false);
            yield return StartCoroutine(DiceUI.Instance.OnDiceClickCoroutine(false, .75f));
            if (isDoubleNextRoll)
            {
                luckyDiceResult *= 2;
            }
            ResourceManager.Instance.UpdateGui();
            if (!CharacterMovement.Instance.isReverse && (!CharacterMovement.Instance.isStunning || diceResult % 2 == 0))
            {
                CharacterMovement.Instance.MovePlayer(luckyDiceResult);
                isDoubleNextRoll = false;
            }
            else
            {
                CharacterMovement.Instance.BackPlayer(luckyDiceResult);
            }
        }
        luckyDiceResult = 0;
    }
}
