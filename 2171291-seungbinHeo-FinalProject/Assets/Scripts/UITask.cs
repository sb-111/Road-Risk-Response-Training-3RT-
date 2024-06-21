using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public Text taskTitleText;          // ����
    public Text taskDescriptionText;    // ����
    public Text timeText;               // ���ѽð�
    public GameObject checkmark;        // üũ��ũ
    public GameObject xmark;            // x ��ũ
    private Task task;                  // �޾ƿ� task

    private void Update()
    {
        if (task != null)
        {
            // Task�� �Ϸ���� �ʾҰ�, �ð� �ʰ��� �ƴϸ� ���� �ð� ������Ʈ
            if (!task.IsComplete() && !task.IsTimeOver())
            {
                UpdateUI(); // �ð� text ������Ʈ
            }
        }
    }
    // Task UI�� �����Ѵ�.
    public void SetTaskUI(Task task)
    {
        this.task = task;                                   // task �޾ƿ���
        taskTitleText.text = task.GetTitle();               // task�� ���� ���� ����
        taskDescriptionText.text = task.GetDescription();   // task�� ���� ���� ����
    }
    // Task UI�� �Ϸ� ���θ� ������Ʈ�Ѵ�.
    public void UpdateUI()
    {
        if (task.IsComplete()) // ���� �ð��� �Ϸ� �� üũ ǥ�� (TaskManager���� �� ���)
        {
            checkmark.SetActive(true);
            
        }
        else if(task.IsTimeOver()) // �ð� �ʰ� �� x ǥ�� (TaskManager���� �� ���)
        {
            xmark.SetActive(true);
        }
        else // �Ϸᵵ �ƴϰ�, Ÿ�ӿ����� �ƴ� �� (�ش� Update������ ���� �����)
        {
            timeText.text = $"{task.GetTimeRemaining():F1} ��";
        }
    }
    
}
