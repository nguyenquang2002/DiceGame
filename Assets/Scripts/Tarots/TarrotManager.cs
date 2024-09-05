using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarrotManager : MonoBehaviour
{
    private static TarrotManager _instance;

    public static TarrotManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TarrotManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("TarrotManager");
                    _instance = go.AddComponent<TarrotManager>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] Transform card1Pos, card2Pos, card3Pos;
    [SerializeField] List<TarrotCard> allTarotCards;
    [SerializeField] Button okButton;
    public List<TarrotCard> displayedCards = new List<TarrotCard>(3);
    public List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    public void DisplayRandomTarotCards()
    {
        displayedCards.Clear();
        gameObjects.Clear();
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, allTarotCards.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);
            displayedCards.Add(allTarotCards[randomIndex]);
        }
        gameObjects.Add(Instantiate(displayedCards[0].gameObject, card1Pos));
        gameObjects.Add(Instantiate(displayedCards[1].gameObject, card2Pos));
        gameObjects.Add(Instantiate(displayedCards[2].gameObject, card3Pos));
        okButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void OKClick()
    {
        foreach(GameObject game in gameObjects)
        {
            Destroy(game);
        }
        if (TarrotCard.selectedCard != null)
        {
            TarrotCard.selectedCard.ApplyEffect();
            okButton.gameObject.SetActive(false); 
        }
        gameObjects.Clear();
        gameObject.SetActive(false);
    }

    public void DisplayOKButton()
    {
        okButton.gameObject.SetActive(true);
    }
}
