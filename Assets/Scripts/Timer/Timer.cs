using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer UI")]
    [SerializeField] Text timerText;

    int timeElapsed = 0;
    bool isTimerEnabled = true;

    // Cached Components
    PlayerSizeSwitcher playerSizeSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        playerSizeSwitcher = GetComponent<PlayerSizeSwitcher>();
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        while(true) 
        {
            if (!isTimerEnabled) yield return null;
            timerText.text = timeElapsed.ToString();
            yield return new WaitForSeconds(1f);
            timeElapsed++;
            playerSizeSwitcher.HandleSizeRandomizer(timeElapsed);
        }
    }

    // MM:SS
    public string GetTimeString() 
    {
        if (timeElapsed < 60) return ("00:" + timeElapsed);
        float t = timeElapsed;
        float seconds = Mathf.Floor(t % 60);
        t /= 60;
        float minutes = Mathf.Floor(t % 60);

        string minutesText = minutes < 10 ? "0" + minutes : minutes.ToString();
        string secondsText = seconds < 10 ? "0" + seconds : seconds.ToString();

        return minutesText + ":" + secondsText;
    }

}
