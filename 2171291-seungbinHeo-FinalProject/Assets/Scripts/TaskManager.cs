using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    //private List<Task> tasks = new List<Task>();
    public GameObject taskPrefab; // Task UI Prefab
    public Transform taskListContent; // ScrollView�� Content

    private List<UITask> tasks = new List<UITask>();
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
        //tasks.Add(new Task(title, description));
        GameObject newTask = Instantiate(taskPrefab, taskListContent);
        UITask taskUI = newTask.GetComponent<UITask>();
        taskUI.SetTaskPanel(title, description);
        tasks.Add(taskUI);
    }
    // Task �Ϸ�
    public void CompleteTask(int index)
    {
        //if (index >= 0 && index < tasks.Count)
        //{
        //    // index��° Task�� �Ϸ�ó��
        //    tasks[index].SetComplete();
        //    // UIManager�� �˸�
        //    UIManager.Instance.UpdateTaskStatus(index, true);
        //}
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].SetTaskComplete(true);
        }
    }
    // Task �迭 ��ȯ
    //public List<Task> GetTasks()
    //{
    //    return tasks;
    //}
    // Task �迭 ����
    public void ResetTasks()
    {
        tasks.Clear();
    }
}
//public class Task
//{
//    private string title;      // ����
//    private string description; // ����
//    private bool isCompleted;   // �Ϸ�Ǿ�����

//    public Task(string title, string description)
//    {
//        this.title = title;
//        this.description = description;
//        isCompleted = false;
//    }
//    public void SetComplete()
//    {
//        this.isCompleted = true;
//    }
//}