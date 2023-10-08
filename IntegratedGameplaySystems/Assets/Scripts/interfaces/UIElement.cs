using UnityEngine;
using TMPro;

[System.Serializable]
public class UIElement
{
    public void UpdateElement(float count)
    {
        uiText.text = count.ToString();
    }

    public void Initialize()
    {
        GameObject = GameObject.Instantiate(uiElementPrefab);

        uiText = GameObject.GetComponent<TMP_Text>();
    }

    public TMP_Text uiText { get; set; }
    public GameObject uiElementPrefab { get; set; }
    public GameObject GameObject { get; set; }
}
