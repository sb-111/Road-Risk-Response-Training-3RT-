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
        // ��Ȳ ���� ������ ��(���� ���� �ʱ�)
        if (!isSituationStart)
        {
            // F1Ű ������ ��Ȳ ���� �г� ���
            if(Input.GetKeyDown(KeyCode.F1)) 
            {
                UIManager.Instance.ToggleSituationSelctionPanel();
            }
        }
        // ��Ȳ ���� ���� ��
        if(isSituationStart)
        {
            // ��Ű ������ ���� ��� �г� ���
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance.ToggleTaskPanel();
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

        //
        //uiTimer?.gameObject.SetActive(true); // Ÿ�̸� Ȱ��
        //uiTimer?.StartTimer(); // Ÿ�̸� ����

        AddTasks(currentSituation); // ��Ȳ ��ٰ� �����ϰ�
    }
    private void InitializeGame()
    {
        ScoreManager.Instance.ResetScore();
        TaskManager.Instance.ResetTasks();
    }
   public void AddTasks(Situation situation)
    {
        TaskManager.Instance.AddTask("���", "������ ��������");
    }
    // called by UITimer
    public void EndSituation()
    {
        int finalScore = ScoreManager.Instance.GetScore();   // ���� ���� ��������
        List<string> mistakes = ScoreManager.Instance.GetMistakes(); // �Ǽ� �׸� ��������

        string feedback = "Ʋ�� ����: \n";

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