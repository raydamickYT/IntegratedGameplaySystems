using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerScript
{
    private readonly Stopwatch timer = new();
    public double BonusTime = 0;
    private GameManager owner;
    private TimerData timerData;

    public TimerScript(GameManager owner, Transform parentTransform, TimerData timerData)
    {
        owner.UiElementsData.TimerUIElement = GameObject.Instantiate(owner.UiElementsData.UiElementsPrefabs[1], parent: parentTransform);
        this.timerData = timerData;

        this.owner = owner;
        if (owner.UiElementsData.TimerUIText == null)
        {
            owner.UiElementsData.TimerUIText = owner.UiElementsData.TimerUIElement.GetComponent<TMP_Text>();
        }

        owner.GameOverEvent += StopTimer;
        timer.Start();
    }

    public void StartTimer()
    {
        timer.Start();
    }

    public double ReturnTimeInSeconds()
    {
        return timer.Elapsed.TotalSeconds;
    }

    public void UpdateTimerElement()
    {
        if (owner.UiElementsData.TimerUIText == null) { return; }
        UnityEngine.Debug.Log(BonusTime);

        var UiTimer = $@"Timer = {Mathf.RoundToInt((float)timer.Elapsed.TotalSeconds - (float)BonusTime)}";
        owner.UiElementsData.TimerUIText.text = UiTimer;

       // UnityEngine.Debug.Log((float)TimeValue);

        if (BonusTime != 0)
        {
            BonusTime = 0;
        }
        if (Mathf.RoundToInt((float)timer.Elapsed.TotalSeconds - (float)BonusTime) > timerData.TimeUntilGameOver)
        {
            owner.GameOverEvent?.Invoke();
        }
    }

    public void StopTimer()
    {
        timer.Stop();
    }
}