using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PopupController : MonoBehaviour
{
    float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAndDelete());
        
    }

    IEnumerator FadeAndDelete() {
        Image popup = gameObject.transform.GetComponent<Image>();
        Color initialColor = popup.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        TextMeshProUGUI popupText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Color initialTextColor = popupText.color;
        Color targetTextColor = new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            popup.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            popupText.color = Color.Lerp(initialTextColor, targetTextColor, elapsedTime / fadeDuration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
