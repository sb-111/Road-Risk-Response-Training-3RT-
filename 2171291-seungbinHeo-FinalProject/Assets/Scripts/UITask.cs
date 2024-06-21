using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public Text taskTitleText;          // 제목
    public Text taskDescriptionText;    // 내용
    public Text timeText;               // 제한시간
    public GameObject checkmark;        // 체크마크
    public GameObject xmark;            // x 마크
    private Task task;                  // 받아온 task

    private void Update()
    {
        if (task != null)
        {
            // Task가 완료되지 않았고, 시간 초과도 아니면 남은 시간 업데이트
            if (!task.IsComplete() && !task.IsTimeOver())
            {
                UpdateUI(); // 시간 text 업데이트
            }
        }
    }
    // Task UI를 설정한다.
    public void SetTaskUI(Task task)
    {
        this.task = task;                                   // task 받아오기
        taskTitleText.text = task.GetTitle();               // task에 대한 제목 설정
        taskDescriptionText.text = task.GetDescription();   // task에 대한 설명 설정
    }
    // Task UI의 완료 여부를 업데이트한다.
    public void UpdateUI()
    {
        if (task.IsComplete()) // 제한 시간내 완료 시 체크 표시 (TaskManager에서 온 경우)
        {
            checkmark.SetActive(true);
            
        }
        else if(task.IsTimeOver()) // 시간 초과 시 x 표시 (TaskManager에서 온 경우)
        {
            xmark.SetActive(true);
        }
        else // 완료도 아니고, 타임오버도 아닐 때 (해당 Update문에서 오는 경우임)
        {
            timeText.text = $"{task.GetTimeRemaining():F1} 초";
        }
    }
    
}
