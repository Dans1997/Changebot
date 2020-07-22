using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer UI")]
    [SerializeField] Text timerText;

    int timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        while(true) 
        {
            timerText.text = timeElapsed.ToString();
            yield return new WaitForSeconds(1f);
            timeElapsed++;
        }
    }


}
