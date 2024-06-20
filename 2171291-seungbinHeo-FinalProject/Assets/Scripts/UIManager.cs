using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance; // 싱글톤 패턴

    //[SerializeField] private GameObject situationSelectionUIObj; // 상황 선택 UI 
    //[SerializeField] private GameObject notificationUIObj; // 특정 사물 근처 알림 UI
    //[SerializeField] private GameObject timerUIObj; // 타이머 UI 
    //[SerializeField] private GameObject missionsUIObj; // 미션 UI 
    //[SerializeField] private GameObject resultUIObj; // 결과창 UI 

    //private GameObject currentNotificationUI; // 현재 표시 중인 알림 UI
    //private GameObject currentResultUI; // 현재 표시 중인 결과창 UI
    //private GameObject currentTimerUI; // 현재 표시 중인 타이머 UI

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
    //    // 특정 위치에 알림 UI를 생성하여 표시
    //    if (currentNotificationUI != null)
    //    {
    //        Destroy(currentNotificationUI);
    //    }

    //    currentNotificationUI = Instantiate(notificationUIObj, position, Quaternion.identity);
    //    // 알림 메시지 설정
    //    currentNotificationUI.GetComponent<NotificationUI>().SetMessage(message);
    //}

    //public void ShowResultUI(int score)
    //{
    //    // 결과창 UI를 생성하여 표시
    //    if (currentResultUI != null)
    //    {
    //        Destroy(currentResultUI);
    //    }

    //    currentResultUI = Instantiate(resultUIObj, Vector3.zero, Quaternion.identity);
    //    // 결과 메시지 설정
    //    currentResultUI.GetComponent<ResultUI>().SetScore(score);
    //}

    //public void ShowTimerUI()
    //{
    //    // 타이머 UI를 생성하여 표시
    //    if (currentTimerUI != null)
    //    {
    //        Destroy(currentTimerUI);
    //    }

    //    currentTimerUI = Instantiate(timerUIObj, Vector3.zero, Quaternion.identity);
    //}

    //// 기타 UI 관련 메서드들...
    ///


    public static UIManager Instance;
    [Header("상황 선택 패널 UI")]
    [SerializeField] private GameObject situationSelectionPanel; // 상황선택 패널
    [Header("타이머 UI")]
    [SerializeField] private GameObject timerPanel; // 타이머 패널
    [SerializeField] private Text timerText;        // 타이머 텍스트
    [Header("스크롤뷰 UI")]
    [SerializeField] private GameObject taskPanel;      // 할일 목록 패널
    [SerializeField] private GameObject taskScrollView; // 할일 목록 스크롤뷰
    [Header("피드백 패널 UI")]
    [SerializeField] private GameObject feedbackPanel; // 피드백 패널
    [SerializeField] private Text feedbackText; // 피드백 텍스트
    [SerializeField] private Image[] feedbackStars; // 피드백 별
    [SerializeField] private Text feedbackScore; // 피드백 점수

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
    // 상황 선택 패널 토글
    public void ToggleSituationSelctionPanel()
    {
        situationSelectionPanel.SetActive(!situationSelectionPanel.activeSelf);
    }
    
    public void UpdateTaskStatus(int taskIndex, bool isCompleted)
    {
        Transform content = taskScrollView.transform.Find("Viewport/Content"); // 스크롤뷰 하위 Content 찾기
        Transform taskItem = content.GetChild(taskIndex); // Content 하위 인덱스번호에 해당하는 Item 가져오기
        Transform checkmarkBackground = taskItem.Find("CheckmarkBackground"); // Item의 CheckmarkBackground 찾기
        Transform checkmark = checkmarkBackground.Find("Checkmark"); // CheckmarkBackground 에서 Checkmark 찾기
        checkmark.gameObject.SetActive(isCompleted); // 해당 게임 오브젝트 활성화

    }
    //// Task Panel 활성
    //public void ShowTaskScrollView()
    //{
    //    taskPanel.SetActive(true);
    //}
    //// Task Panel 비활성
    //public void HideTaskScrollView()
    //{
    //    taskPanel.SetActive(false);
    //}
    // Task Panel 토글
    public void ToggleTaskPanel()
    {
        taskPanel.SetActive(!taskPanel.activeSelf);
    }
    // 피드백 보여주는 메서드
    public void ShowFeedback(string feedback, int starCount, int score)
    {
        feedbackText.text = feedback;
        for(int i=0; i<starCount; i++) // 나온 별 개수까지만 노란색
        {
            feedbackStars[i].color = Color.yellow;
        }
        feedbackScore.text = score.ToString();
        feedbackPanel.SetActive(true);
    }
    // 피드백 숨기는 메서드
    public void HideFeedback()
    {
        feedbackPanel.SetActive(false);
    }
}
