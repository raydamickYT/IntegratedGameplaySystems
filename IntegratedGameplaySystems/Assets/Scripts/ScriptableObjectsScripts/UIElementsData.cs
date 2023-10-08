using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UIElementsData", menuName = "ScriptableObjects/UIElementsData")]
public class UIElementsData : ScriptableObject
{
    public List<GameObject> UiElementsPrefabs = new();

    [NonSerialized] public GameObject SpeedUIElement;
    [NonSerialized] public TMP_Text SpeedUIText;

    [NonSerialized] public GameObject TimerUIElement;
    [NonSerialized] public TMP_Text TimerUIText;
}
