using System.Diagnostics;
using TMPro;
using UnityEngine;

public class TimerScript
{
    private readonly Stopwatch timer = new();
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

        owner.UiElementsData.TimerUIText.text = $@"Timer = {Mathf.RoundToInt((float)timer.Elapsed.TotalSeconds)}";

        if (Mathf.RoundToInt((float)timer.Elapsed.TotalSeconds) > timerData.TimeUntilGameOver)
        {
            owner.GameOverEvent?.Invoke();
        }
    }

    public void StopTimer()
    {
        timer.Stop();
    }
}