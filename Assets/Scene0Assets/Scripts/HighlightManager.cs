using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighlightManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonImagePair
    {
        public Button button;
        public Image imageToHighlight;
    }

    public List<ButtonImagePair> pairs;
    public Color highlightColor = Color.yellow;
    public float highlightDuration = 1.0f;

    private void Start()
    {
        foreach (var pair in pairs)
        {
            if (pair.button != null && pair.imageToHighlight != null)
            {
                pair.imageToHighlight.gameObject.SetActive(false); // Hide at start
                pair.button.onClick.AddListener(() => HandleClick(pair.imageToHighlight));
            }
        }
    }

    private void HandleClick(Image image)
    {
        foreach (var pair in pairs)
        {
            if (pair.imageToHighlight != null)
            {
                pair.imageToHighlight.gameObject.SetActive(false);
            }
        }
        image.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HighlightEffect(image));
    }

    private IEnumerator HighlightEffect(Image image)
    {
        Color originalColor = image.color;
        image.color = highlightColor;
        yield return new WaitForSeconds(highlightDuration);
        image.color = originalColor;
    }
}
