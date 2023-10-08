using TMPro;
using UnityEngine;

public class SpeedUIManager
{
    private Player owner;

    public SpeedUIManager(Player owner, Transform parentTransform)
    {
        this.owner = owner;

        owner.UIElementsData.SpeedUIElement = GameObject.Instantiate(owner.UIElementsData.UiElementsPrefabs[0], parent: parentTransform);

        owner.UIElementsData.SpeedUIText = owner.UIElementsData.SpeedUIElement.GetComponent<TMP_Text>();
    }

    public void UpdateSpeedUIElement(float speed)
    {
        if (owner.UIElementsData.SpeedUIText == null) { return; }

        owner.UIElementsData.SpeedUIText.text = $"Current Speed = {Mathf.RoundToInt(speed)}";
    }
}
