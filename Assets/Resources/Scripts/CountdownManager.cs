using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public static CountdownManager Instance { get; private set; }
    [SerializeField] TMP_Text currentTimerTextDisplay;
    [SerializeField] TMP_Text timeTotalTextDisplay;

    // How many seconds per round
    private readonly int timeToCountDown = 180;

    private int remainingTime;
    public int RemainingTime { get { return remainingTime; } private set { remainingTime = value; } }

    private int totalTime = 0;
    public int TotalTime { get { return totalTime; } private set { totalTime = value; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        // Set remaining time to three minutes
        remainingTime = timeToCountDown;

        // Add current spent time doing tasks to totalTime
        totalTime += timeToCountDown;

        // Remove three minutes to display CURRENT amount of time spent doing tasks
        timeTotalTextDisplay.text = $"{(totalTime-180) / 60} Minuten von 60 Minuten absolviert.";
        StartCoroutine(Countdown());
    }

    /// <summary>
    /// Countdown and update timer displays.
    /// </summary>
    /// <returns>Waits for 1 second</returns>
    private IEnumerator Countdown()
    {
        string displayTime;

        //Debug Only
        //WaitForSeconds timeTick = new WaitForSeconds(1f / ResultsManager.Instance.Speed);

        WaitForSeconds timeTick = new WaitForSeconds(1f);

        while (remainingTime > 0)
        {
            displayTime =  FormatRemainingTime(remainingTime);
            currentTimerTextDisplay.text = $"{displayTime}";

            yield return timeTick;

            remainingTime--;
            displayTime = FormatRemainingTime(remainingTime);
            currentTimerTextDisplay.text = $"{displayTime}";
        }
    }

    /// <summary>
    /// Formats text to display time left.
    /// </summary>
    /// <param name="remainingSeconds"></param>
    /// <returns></returns>
    public string FormatRemainingTime(int remainingSeconds)
    {
        if (remainingSeconds > 120 && remainingSeconds <= 180) // Between 120 and 180 seconds
        {
            return "3 Minuten";
        }
        else if (remainingSeconds > 60 && remainingSeconds <= 120) // Between 60 and 120 seconds
        {
            return "2 Minuten";
        }
        else if (remainingSeconds > 0 && remainingSeconds <= 60) // Between 0 and 60 seconds
        {
            return "<1 Minute";
        }
        else 
        {
            return "Zeit ist abgelaufen.";
        }
    }
}
