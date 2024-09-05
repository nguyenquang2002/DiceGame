using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    private static ResourceDisplay _instance;

    public static ResourceDisplay Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourceDisplay>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("ResourceDisplay");
                    _instance = go.AddComponent<ResourceDisplay>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] Image imageDisplay;
    [SerializeField] TextMeshProUGUI textDisplay;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

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
        //imageDisplay = transform.GetChild(1).GetComponent<Image>();
        //textDisplay = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }


    public void UpdateGUI(Image image, string text)
    {
        if (imageDisplay != null)
        {
            imageDisplay.sprite = image.sprite;
        }
        if(text != null)
        {
            textDisplay.text = text;
        }
        else
        {
            textDisplay.text = "";
        }
    }

    public void DisplayEffect()
    {
        canvasGroup.alpha = 1;
        gameObject.SetActive(true);
        StartCoroutine(Fade(canvasGroup,0f,1f,50f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay, float rectY)
    {
        yield return new WaitForSeconds(delay);
        
        float elapsed = 0f;
        float duration = 1f;
        float from = canvasGroup.alpha;
        Vector3 tempPosition = rectTransform.position;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            rectTransform.position = Vector3.Lerp(rectTransform.position, tempPosition +
                new Vector3(0, rectY), elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
        rectTransform.position = tempPosition;
        gameObject.SetActive(false);
    }
}
