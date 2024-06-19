using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private bool isTimerStart = false;
    private float timer = 180f;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerStart)
        {
            timer -= Time.deltaTime;
            UpdateUI();
        }
    }
    public void StartTimer()
    {
        isTimerStart = true;

    }
    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
