using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    private List<Task> tasks = new List<Task>();
    private Situation situation;
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
    public void InitailizeTask(Situation situation)
    {
        this.situation = situation;
        switch (situation)
        {
            case Situation.Accident:

                break;
            case Situation.Winter:
                
                break;
            case Situation.Breakdown:
                
                break;
        }
    }
    // Task �߰�
    public void AddTask(string title ,string description)
    {
        tasks.Add(new Task(title, description));
    }
    // Task �Ϸ�
    public void CompleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            // index��° Task�� �Ϸ�ó��
            tasks[index].SetComplete();
            // UIManager�� �˸�
            UIManager.Instance.UpdateTaskStatus(index, true);
        }
    }
    // Task �迭 ��ȯ
    public List<Task> GetTasks()
    {
        return tasks;
    }
    // Task �迭 ����
    public void ResetTasks()
    {
        tasks.Clear();
    }
}
public class Task
{
    private string title;      // ����
    private string description; // ����
    private bool isCompleted;   // �Ϸ�Ǿ�����

    public Task(string title, string description)
    {
        this.title = title;
        this.description = description;
        isCompleted = false;
    }
    public void SetComplete()
    {
        this.isCompleted = true;
    }
}