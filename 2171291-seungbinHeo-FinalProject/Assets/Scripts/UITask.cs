using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public Text taskTitleText;
    public Text taskDescriptionText;
    public GameObject checkmark;

    // Task ���� ����
    public void SetTaskPanel(string title, string description)
    {
        taskTitleText.text = title;
        taskDescriptionText.text = description;
    }

    // Task �Ϸ� ���� ����
    public void SetTaskComplete(bool isComplete)
    {
        // true ���� �������� ����
        checkmark.SetActive(isComplete);
    }
}
