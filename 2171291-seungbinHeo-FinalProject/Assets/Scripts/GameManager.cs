using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UITimer uiTimer;                 // Ÿ�̸� ��ũ��Ʈ
    private bool isSituationStart = false;  // ��Ȳ�� �����ߴ���
    private Situation currentSituation;     // � ��Ȳ�� ��������
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
        // ��Ȳ ���� ������ ��(���� ���� �ʱ�)
        if (!isSituationStart)
        {
            // F1Ű ������ ��Ȳ ���� �г� ���
            if (Input.GetKeyDown(KeyCode.F1))
            {
                UIManager.Instance.ToggleSituationSelctionPanel();
            }
        }
        // ��Ȳ ���� ���� ��
        if (isSituationStart)
        {
            // ��Ű ������ ���� ��� �г� ���
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ToggleTaskPanel();
            }
            // ��� Task���� �Ϸ�Ǿ��ٸ� ��������
            if (TaskManager.Instance.IsCompleteAllTasks())
            {
                StopTimer();
            }
        }
    }
    // ��Ȳ ���� ��ư ������ ��
    public void SelectSituation(Situation situation)
    {
        UIManager.Instance.ToggleSituationSelctionPanel(); // ��Ȳ ���� �г� ������
        InitializeGame(); // �Ŵ��� �ʱ�ȭ
        currentSituation = situation; // ��Ȳ ����
        isSituationStart = true; // ��Ȳ ���� �÷��� ����


        //AddTasks(currentSituation); // ��Ȳ ��ٰ� �����ϰ�
        if(currentSituation == Situation.Accident)
        {
            TaskManager.Instance.AddTask("���", "������ ��������", 0, "none", 20f);
            
        }
        else if(currentSituation == Situation.Winter)
        {

        }
        else if(currentSituation == Situation.Breakdown)
        {

        }
        
    }
    // Ÿ�̸� Ȱ�� �� ����
    public void StartTimer()
    {
        uiTimer.gameObject.SetActive(true);
        uiTimer.StartTimer();
    }
    // ���� �ð����� ��� �� ������ ��� ȣ��� (���� ����)
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
        //int finalScore = ScoreManager.Instance.GetScore();   // ���� ���� ��������
        //List<string> mistakes = ScoreManager.Instance.GetMistakes(); // �Ǽ� �׸� ��������
        int finalScore = 100;
        string feedback = "Ʋ�� ����: \n";

        List<Task> tasks = TaskManager.Instance.GetTasks();
        foreach (Task task in tasks)
        {
            if (task.IsTimeOver()) // �ð� ���� �Ϸ����� ���� �ǿ� ���� �ø�
            {
                string mistake = task.GetMistake();
                feedback += $"  - {mistake}\n"; // �ǵ�鿡 �ݿ�
                finalScore -= task.GetScore();  // ���� �谨
            }
            //if (task.IsTryBad()) // �Ǽ��Ѱ� �ִٸ�
            //{
            //    string mistake = task.GetMistake();
            //    feedback += $"  - {mistake}\n"; // �ǵ�鿡 �ݿ�
            //    finalScore -= task.GetScore();  // ���� �谨
            //}
            //else if (!task.IsComplete()) // �Ϸ���� ���� Task
            //{
            //    string mistake = $"{task.GetMistake()} (�õ����� ����)";
            //    feedback += $"  - {mistake}\n"; // �ǵ�鿡 �ݿ�
            //    finalScore -= task.GetScore();  // ���� �谨
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
    // ��� Task�� �Ϸ��ߴ��� Ȯ��
}
public enum Situation
{
    Accident, Winter, Breakdown
}