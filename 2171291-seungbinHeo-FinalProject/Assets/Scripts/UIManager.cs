using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance; // �̱��� ����

    //[SerializeField] private GameObject situationSelectionUIObj; // ��Ȳ ���� UI 
    //[SerializeField] private GameObject notificationUIObj; // Ư�� �繰 ��ó �˸� UI
    //[SerializeField] private GameObject timerUIObj; // Ÿ�̸� UI 
    //[SerializeField] private GameObject missionsUIObj; // �̼� UI 
    //[SerializeField] private GameObject resultUIObj; // ���â UI 

    //private GameObject currentNotificationUI; // ���� ǥ�� ���� �˸� UI
    //private GameObject currentResultUI; // ���� ǥ�� ���� ���â UI
    //private GameObject currentTimerUI; // ���� ǥ�� ���� Ÿ�̸� UI

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void ShowNotificationUI(Vector3 position, string message)
    //{
    //    // Ư�� ��ġ�� �˸� UI�� �����Ͽ� ǥ��
    //    if (currentNotificationUI != null)
    //    {
    //        Destroy(currentNotificationUI);
    //    }

    //    currentNotificationUI = Instantiate(notificationUIObj, position, Quaternion.identity);
    //    // �˸� �޽��� ����
    //    currentNotificationUI.GetComponent<NotificationUI>().SetMessage(message);
    //}

    //public void ShowResultUI(int score)
    //{
    //    // ���â UI�� �����Ͽ� ǥ��
    //    if (currentResultUI != null)
    //    {
    //        Destroy(currentResultUI);
    //    }

    //    currentResultUI = Instantiate(resultUIObj, Vector3.zero, Quaternion.identity);
    //    // ��� �޽��� ����
    //    currentResultUI.GetComponent<ResultUI>().SetScore(score);
    //}

    //public void ShowTimerUI()
    //{
    //    // Ÿ�̸� UI�� �����Ͽ� ǥ��
    //    if (currentTimerUI != null)
    //    {
    //        Destroy(currentTimerUI);
    //    }

    //    currentTimerUI = Instantiate(timerUIObj, Vector3.zero, Quaternion.identity);
    //}

    //// ��Ÿ UI ���� �޼����...
    ///


    public static UIManager Instance;
    [Header("��Ȳ ���� �г� UI")]
    [SerializeField] private GameObject situationSelectionPanel; // ��Ȳ���� �г�
    [Header("Ÿ�̸� UI")]
    [SerializeField] private GameObject timerPanel; // Ÿ�̸� �г�
    [SerializeField] private Text timerText;        // Ÿ�̸� �ؽ�Ʈ
    [Header("��ũ�Ѻ� UI")]
    [SerializeField] private GameObject taskPanel;      // ���� ��� �г�
    [SerializeField] private GameObject taskScrollView; // ���� ��� ��ũ�Ѻ�
    [Header("�ǵ�� �г� UI")]
    [SerializeField] private GameObject feedbackPanel; // �ǵ�� �г�
    [SerializeField] private Text feedbackText; // �ǵ�� �ؽ�Ʈ
    [SerializeField] private Image[] feedbackStars; // �ǵ�� ��
    [SerializeField] private Text feedbackScore; // �ǵ�� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ��Ȳ ���� �г� ���
    public void ToggleSituationSelctionPanel()
    {
        situationSelectionPanel.SetActive(!situationSelectionPanel.activeSelf);
    }
    
    public void UpdateTaskStatus(int taskIndex, bool isCompleted)
    {
        Transform content = taskScrollView.transform.Find("Viewport/Content"); // ��ũ�Ѻ� ���� Content ã��
        Transform taskItem = content.GetChild(taskIndex); // Content ���� �ε�����ȣ�� �ش��ϴ� Item ��������
        Transform checkmarkBackground = taskItem.Find("CheckmarkBackground"); // Item�� CheckmarkBackground ã��
        Transform checkmark = checkmarkBackground.Find("Checkmark"); // CheckmarkBackground ���� Checkmark ã��
        checkmark.gameObject.SetActive(isCompleted); // �ش� ���� ������Ʈ Ȱ��ȭ

    }
    //// Task Panel Ȱ��
    //public void ShowTaskScrollView()
    //{
    //    taskPanel.SetActive(true);
    //}
    //// Task Panel ��Ȱ��
    //public void HideTaskScrollView()
    //{
    //    taskPanel.SetActive(false);
    //}
    // Task Panel ���
    public void ToggleTaskPanel()
    {
        taskPanel.SetActive(!taskPanel.activeSelf);
    }
    // �ǵ�� �����ִ� �޼���
    public void ShowFeedback(string feedback, int starCount, int score)
    {
        feedbackText.text = feedback;
        for(int i=0; i<starCount; i++) // ���� �� ���������� �����
        {
            feedbackStars[i].color = Color.yellow;
        }
        feedbackScore.text = score.ToString();
        feedbackPanel.SetActive(true);
    }
    // �ǵ�� ����� �޼���
    public void HideFeedback()
    {
        feedbackPanel.SetActive(false);
    }
}
