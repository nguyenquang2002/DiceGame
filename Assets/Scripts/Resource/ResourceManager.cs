using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;

    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourceManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("ResourceManager");
                    _instance = go.AddComponent<ResourceManager>();
                }
            }
            return _instance;
        }
    }


    public int whiteDice = 3, redDice;
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    [SerializeField] TextMeshProUGUI whiteDiceText, redDiceText;


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
        UpdateGui();
    }

    public void UpdateGui()
    {
        whiteDiceText.text = whiteDice.ToString();
        redDiceText.text = redDice.ToString();
    }

    public void AddResource(string resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += amount;
        }
        else
        {
            resources[resourceType] = amount;
        }
        //foreach(KeyValuePair<string, int> kvp in resources)
        //{
        //    Debug.Log(kvp.Key + ": " + kvp.Value);
        //}
    }
}
