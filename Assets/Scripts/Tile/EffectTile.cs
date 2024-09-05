using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTile : MonoBehaviour
{
    public bool isResource, isLevelUp;
    [SerializeField] Sprite resource, levelUp, levelDown;
    private Image img;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        img = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Effect(float fromY, float toY, float duration)
    {
        if (img != null)
        {
            if (isResource)
            {
                img.sprite = resource;
            }
            else
            {
                if (isLevelUp)
                {
                    img.sprite = levelUp;
                }
                else
                {
                    img.sprite = levelDown;
                }
            }
        }
        StartCoroutine(EffectCoroutine(fromY, toY, duration));
    }
    private IEnumerator EffectCoroutine(float fromY, float toY, float duration)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, fromY);
        float timer = 0f;
        while (timer < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, toY), timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
