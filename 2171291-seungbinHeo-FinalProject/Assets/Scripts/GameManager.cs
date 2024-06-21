using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UITimer uiTimer;
    private bool isSituationStart = false;
    private Situation currentSituation;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        // 상황 시작 안했을 때(게임 시작 초기)
        if (!isSituationStart)
        {
            // F1키 누르면 상황 선택 패널 토글
            if(Input.GetKeyDown(KeyCode.F1)) 
            {
                UIManager.Instance.ToggleSituationSelctionPanel();
            }
        }
        // 상황 시작 했을 때
        if(isSituationStart)
        {
            // 탭키 누르면 할일 목록 패널 토글
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ToggleTaskPanel();
            }

        }
    }
    // 상황 시작 버튼 눌렀을 때
    public void SelectSituation(Situation situation)
    {
        UIManager.Instance.ToggleSituationSelctionPanel(); // 상황 선택 패널 내리기
        InitializeGame(); // 매니저 초기화
        currentSituation = situation; // 상황 설정
        isSituationStart = true; // 상황 시작 플래그 설정

        //
        //uiTimer?.gameObject.SetActive(true); // 타이머 활성
        //uiTimer?.StartTimer(); // 타이머 시작

        AddTasks(currentSituation); // 상황 줬다고 가정하고
    }
    private void InitializeGame()
    {
        ScoreManager.Instance.ResetScore();
        TaskManager.Instance.ResetTasks();
    }
   public void AddTasks(Situation situation)
    {
        TaskManager.Instance.AddTask("출발", "여행을 떠나보자");
    }
    // called by UITimer
    public void EndSituation()
    {
        int finalScore = ScoreManager.Instance.GetScore();   // 최종 점수 가져오기
        List<string> mistakes = ScoreManager.Instance.GetMistakes(); // 실수 항목 가져오기

        string feedback = "틀린 내용: \n";

        foreach (string mistake in mistakes)
        {
            feedback += "  - " + mistake + "\n";
        }

        int starCount = 0;
        if (finalScore == 0) starCount = 0;
        else if (finalScore < 30) starCount = 1;
        else if (finalScore < 80) starCount = 2;
        else if(finalScore <= 100) starCount = 3;
        UIManager.Instance.ShowFeedback(feedback, starCount, finalScore);
    }
    public void PlayerPerformedTask(int taskIndex, bool isCorrect)
    {
        if (isCorrect)
        {
            TaskManager.Instance.CompleteTask(taskIndex);
        }
        else
        {
            ScoreManager.Instance.AddMistake("Incorrect action for task " + (taskIndex + 1));
        }
    }
}
public enum Situation
{
    Accident, Winter, Breakdown
}