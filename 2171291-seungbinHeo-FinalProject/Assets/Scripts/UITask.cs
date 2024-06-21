using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public Text taskTitleText;
    public Text taskDescriptionText;
    public GameObject checkmark;

    // Task 설명 설정
    public void SetTaskPanel(string title, string description)
    {
        taskTitleText.text = title;
        taskDescriptionText.text = description;
    }

    // Task 완료 상태 설정
    public void SetTaskComplete(bool isComplete)
    {
        // true 값만 들어오도록 주의
        checkmark.SetActive(isComplete);
    }
}
