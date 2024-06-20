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
    // Task 추가
    public void AddTask(string title ,string description)
    {
        tasks.Add(new Task(title, description));
    }
    // Task 완료
    public void CompleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            // index번째 Task를 완료처리
            tasks[index].SetComplete();
            // UIManager에 알림
            UIManager.Instance.UpdateTaskStatus(index, true);
        }
    }
    // Task 배열 반환
    public List<Task> GetTasks()
    {
        return tasks;
    }
    // Task 배열 리셋
    public void ResetTasks()
    {
        tasks.Clear();
    }
}
public class Task
{
    private string title;      // 제목
    private string description; // 설명
    private bool isCompleted;   // 완료되었는지

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