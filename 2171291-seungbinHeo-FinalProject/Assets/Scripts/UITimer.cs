using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private bool isTimerStart = false;
    private bool isTimerEnd = false;
    private float remainTime = 100f;
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �ð� �� ������ 
        if (remainTime <= 0f)
        {
            isTimerStart = false;
            GameManager.Instance.EndSituation();
        }
        if (isTimerStart) // Ÿ�̸� �۵� ���� ��
        {
            remainTime -= Time.deltaTime;
            UpdateUI(); // Ÿ�̸� UI ������Ʈ
        }

    }
    public void StartTimer()
    {
        isTimerStart = true; // ���� �÷���
    }

    public void StopTimter()
    {
        isTimerStart = false;
    }
    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
