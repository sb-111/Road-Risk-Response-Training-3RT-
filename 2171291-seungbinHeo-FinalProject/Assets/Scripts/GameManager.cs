using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UITimer uiTimer;                 // 타이머 스크립트
    private bool isSituationStart = false;  // 상황이 시작했는지
    private Situation currentSituation;     // 어떤 상황을 눌렀는지
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
            if (Input.GetKeyDown(KeyCode.F1))
            {
                UIManager.Instance.ToggleSituationSelctionPanel();
            }
        }
        // 상황 시작 했을 때
        if (isSituationStart)
        {
            // 탭키 누르면 할일 목록 패널 토글
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ToggleTaskPanel();
            }
            // 모든 Task들이 완료되었다면 조기종료
            if (TaskManager.Instance.IsCompleteAllTasks())
            {
                StopTimer();
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


        //AddTasks(currentSituation); // 상황 줬다고 가정하고
        if(currentSituation == Situation.Accident)
        {
            TaskManager.Instance.AddTask("출발", "여행을 떠나보자", 0, "none", 20f);
            
        }
        else if(currentSituation == Situation.Winter)
        {

        }
        else if(currentSituation == Situation.Breakdown)
        {

        }
        
    }
    // 타이머 활성 및 시작
    public void StartTimer()
    {
        uiTimer.gameObject.SetActive(true);
        uiTimer.StartTimer();
    }
    // 제한 시간내에 모두 잘 수행한 경우 호출됨 (게임 종료)
    public void StopTimer()
    {
        uiTimer.StopTimter();
        EndSituation();
    }
    public Situation GetSituation()
    {
        return currentSituation;
    }
    private void InitializeGame()
    {
        //ScoreManager.Instance.ResetScore();
        TaskManager.Instance.ResetTasks();
    }
    // called by UITimer
    public void EndSituation()
    {
        //int finalScore = ScoreManager.Instance.GetScore();   // 최종 점수 가져오기
        //List<string> mistakes = ScoreManager.Instance.GetMistakes(); // 실수 항목 가져오기
        int finalScore = 100;
        string feedback = "틀린 내용: \n";

        List<Task> tasks = TaskManager.Instance.GetTasks();
        foreach (Task task in tasks)
        {
            if (task.IsTimeOver()) // 시간 내에 완료하지 못한 건에 대해 올림
            {
                string mistake = task.GetMistake();
                feedback += $"  - {mistake}\n"; // 피드백에 반영
                finalScore -= task.GetScore();  // 점수 삭감
            }
            //if (task.IsTryBad()) // 실수한게 있다면
            //{
            //    string mistake = task.GetMistake();
            //    feedback += $"  - {mistake}\n"; // 피드백에 반영
            //    finalScore -= task.GetScore();  // 점수 삭감
            //}
            //else if (!task.IsComplete()) // 완료되지 않은 Task
            //{
            //    string mistake = $"{task.GetMistake()} (시도하지 않음)";
            //    feedback += $"  - {mistake}\n"; // 피드백에 반영
            //    finalScore -= task.GetScore();  // 점수 삭감
            //}
        }

        int starCount = 0;
        if (finalScore == 0) starCount = 0;
        else if (finalScore < 30) starCount = 1;
        else if (finalScore < 80) starCount = 2;
        else if(finalScore <= 100) starCount = 3;
        UIManager.Instance.ShowFeedback(feedback, starCount, finalScore);
    }

    //public void PlayerPerformedTask(int taskIndex, bool isCorrect)
    //{
    //    if (isCorrect)
    //    {
    //        TaskManager.Instance.CompleteTask(taskIndex);
    //    }
    //    else
    //    {
    //        //ScoreManager.Instance.AddMistake("Incorrect action for task " + (taskIndex + 1));
    //    }
    //}
    // 모든 Task를 완료했는지 확인
}
public enum Situation
{
    Accident, Winter, Breakdown
}