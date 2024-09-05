using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyDiceUI : MonoBehaviour
{
    public static LuckyDiceUI Instance;

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Button[] diceButtons;
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject tickPrefab;

    private GameObject currentTick;

    private void Awake()
    {
        Instance = this;
        okButton.gameObject.SetActive(false);
        selectionPanel.SetActive(false);

        for (int i = 0; i < diceButtons.Length; i++)
        {
            int diceValue = i + 1;
            diceButtons[i].onClick.AddListener(() => OnDiceButtonClicked(diceValue));
        }

    }

    public void ShowDiceSelectionUI()
    {
        if(ResourceManager.Instance.redDice > 0)
        {
            selectionPanel.SetActive(true);
        }
    }

    public void OnDiceButtonClicked(int diceValue)
    {
        DiceController.Instance.luckyDiceResult = diceValue;
        if (currentTick != null)
        {
            Destroy(currentTick);
        }
        currentTick = Instantiate(tickPrefab, diceButtons[diceValue - 1].transform);
        currentTick.transform.SetAsLastSibling();
        okButton.gameObject.SetActive(true);
    }
    public void OnOKClick()
    {
        okButton.gameObject.SetActive(false);
        if (currentTick != null)
        {
            Destroy(currentTick);
        }
        selectionPanel.SetActive(false);
    }
}
