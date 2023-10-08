using TMPro;
using UnityEngine;

public class TimerScript
{
    private double timer = 0;

    private GameManager owner;
    private TimerData timerData;

    private bool timerIsRunning = false;

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
        owner.GameWonEvent += StopTimer;
        StartTimer();
    }

    public void StartTimer()
    {
        if (!timerIsRunning)
        {
            timer = 0;
            timerIsRunning = true;
        }
    }

    public double ReturnTimeInSeconds()
    {
        return timer;
    }

    public void ReduceTime(float amount)
    {
        if (timer - amount >= 0)
        {
            timer -= amount;
        }
    }

    public void UpdateTimerElement()
    {
        if (owner.UiElementsData.TimerUIText == null) { return; }

        if (timerIsRunning)
        {
            timer += Time.deltaTime;
        }

        owner.UiElementsData.TimerUIText.text = $@"Timer = {Mathf.RoundToInt((float)timer)}";

        if (timer > timerData.TimeUntilGameOver)
        {
            owner.GameOverEvent?.Invoke();
        }
    }

    public void StopTimer()
    {
        if (timerIsRunning)
        {
            timerIsRunning = false;
            timer = 0;
        }
    }
}