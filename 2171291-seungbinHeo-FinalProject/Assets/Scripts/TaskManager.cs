using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Task���� �Ѱ���
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public GameObject taskPrefab; // Task UI Prefab
    public Transform taskListContent; // ScrollView�� Content
    private List<Task> tasks = new List<Task>(); // Task �迭
    private bool isAddedAllTasks = false; // ��� Task�� �߰��Ǿ����� ����
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
    private void Update()
    {
        foreach (Task task in tasks)
        {
            // �Ϸ� x, Ÿ�ӿ��� x, ���ѽð� �ʰ� ��
            if (!task.IsComplete() && !task.IsTimeOver() && Time.time - task.GetStartTime() > task.GetTimeLimit())
            {
                task.TimeOver(); // Ÿ�ӿ��� ����
                int index = tasks.IndexOf(task);
                UpdateTaskUI(index); // Ÿ�ӿ����� UI�� ������Ʈ
            }
        }
    }

    // Task �߰� (�ܺο��� ȣ��)
    public void AddTask(string title ,string description, int score, string mistake, float timeLimit)
    {
        Task newTask = new Task(title, description, score, mistake, timeLimit);
        tasks.Add(newTask); // tasks �迭�� �߰�
        AddTaskToUI(newTask); // task�� �̿��� UI ����
    }
    // ���� �ð� ����� �ش� �ε����� ���� Task �Ϸ� (�ܺο��� ȣ��)
    public void CompleteTask(int index, string text)
    {
        if (index >= 0 && index < tasks.Count) // �ε��� ���� ���� �ְ�
        {
            Task task = tasks[index];
            // �Ϸ� ���߰�, Ÿ�ӿ��� ���߰�, ���ѽð� �����
            if (!task.IsComplete() && !task.IsTimeOver() && Time.time - task.GetStartTime() < task.GetTimeLimit()) 
            {
                tasks[index].Complete(); // �ش� task �Ϸ�ó��
                UpdateTaskUI(index); // �ش� task UI ������Ʈ
                UIManager.Instance.ShowShortPanel(text);
            }
        }
    }
    //public bool CheckTaskCompletion(int index)
    //{
    //    if (index >= 0 && index < tasks.Count) // �ε��� ���� ���� �ְ�
    //    {
    //        Task task = tasks[index];

    //        return task.IsComplete();
    //    }
    //    return false;
    //}
    //// �ش� �ε����� ���� Task �߸��� �õ� (�ܺο��� ȣ��)
    //public void TryBadTask(int index)
    //{
    //    if(index >= 0 && index < tasks.Count)
    //    {
    //        tasks[index].TryBad();
    //    }
    //}

    // UI�� �߰��ϴ� �޼��� (UITask�� ����)
    private void AddTaskToUI(Task task)
    {
        GameObject taskUI = Instantiate(taskPrefab, taskListContent); 
        UITask cshUITask = taskUI.GetComponent<UITask>();
        cshUITask.SetTaskUI(task);
    }
    // UI ������Ʈ �ϴ� �޼��� (UITask�� ����)
    private void UpdateTaskUI(int index)
    {
        UITask cshUITask = taskListContent.GetChild(index).GetComponent<UITask>();
        cshUITask.UpdateUI();
    }

    //Task �迭 ��ȯ
    public List<Task> GetTasks()
    {
        return tasks;
    }
    // Task �迭 ����
    public void ResetTasks()
    {
        isAddedAllTasks = false;
        tasks.Clear();
    }
    // ��� Task���� �߰������� ����
    public void AddAllTasks()
    {
        isAddedAllTasks = true;
    }
    // ��� Task�� �߰��Ǿ����� ���θ� ��ȯ
    public bool IsAddedAllTasks() 
    {
        return isAddedAllTasks;
    }
    // ��� Task���� �Ϸ� ���θ� ��ȯ
    public bool IsCompleteAllTasks()
    {
        // ��� Task�� �߰��� ��쿡�� Ȯ����
        if (isAddedAllTasks)
        {
            foreach(Task task in tasks)
            {
                if (!task.IsComplete())
                {
                    return false;
                }
            }
            return true;
        }
        else return false; 
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

//public void InitailizeTask(Situation situation)
//{
//    this.situation = situation;
//    switch (situation)
//    {
//        case Situation.Accident:

//            break;
//        case Situation.Winter:

//            break;
//        case Situation.Breakdown:

//            break;
//    }
//}