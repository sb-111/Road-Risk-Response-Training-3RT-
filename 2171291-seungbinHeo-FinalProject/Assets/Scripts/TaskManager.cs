using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Task들을 총관리
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public GameObject taskPrefab; // Task UI Prefab
    public Transform taskListContent; // ScrollView의 Content
    private List<Task> tasks = new List<Task>(); // Task 배열
    private bool isAddedAllTasks = false; // 모든 Task가 추가되었는지 여부
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
            // 완료 x, 타임오버 x, 제한시간 초과 시
            if (!task.IsComplete() && !task.IsTimeOver() && Time.time - task.GetStartTime() > task.GetTimeLimit())
            {
                task.TimeOver(); // 타임오버 설정
                int index = tasks.IndexOf(task);
                UpdateTaskUI(index); // 타임오버된 UI를 업데이트
            }
        }
    }

    // Task 추가 (외부에서 호출)
    public void AddTask(string title ,string description, int score, string mistake, float timeLimit)
    {
        Task newTask = new Task(title, description, score, mistake, timeLimit);
        tasks.Add(newTask); // tasks 배열에 추가
        AddTaskToUI(newTask); // task를 이용해 UI 생성
    }
    // 제한 시간 내라면 해당 인덱스에 대한 Task 완료 (외부에서 호출)
    public void CompleteTask(int index, string text)
    {
        if (index >= 0 && index < tasks.Count) // 인덱스 범위 내에 있고
        {
            Task task = tasks[index];
            // 완료 안했고, 타임오버 안했고, 제한시간 내라면
            if (!task.IsComplete() && !task.IsTimeOver() && Time.time - task.GetStartTime() < task.GetTimeLimit()) 
            {
                tasks[index].Complete(); // 해당 task 완료처리
                UpdateTaskUI(index); // 해당 task UI 업데이트
                UIManager.Instance.ShowShortPanel(text);
            }
        }
    }
    //public bool CheckTaskCompletion(int index)
    //{
    //    if (index >= 0 && index < tasks.Count) // 인덱스 범위 내에 있고
    //    {
    //        Task task = tasks[index];

    //        return task.IsComplete();
    //    }
    //    return false;
    //}
    //// 해당 인덱스에 대한 Task 잘못된 시도 (외부에서 호출)
    //public void TryBadTask(int index)
    //{
    //    if(index >= 0 && index < tasks.Count)
    //    {
    //        tasks[index].TryBad();
    //    }
    //}

    // UI에 추가하는 메서드 (UITask에 위임)
    private void AddTaskToUI(Task task)
    {
        GameObject taskUI = Instantiate(taskPrefab, taskListContent); 
        UITask cshUITask = taskUI.GetComponent<UITask>();
        cshUITask.SetTaskUI(task);
    }
    // UI 업데이트 하는 메서드 (UITask에 위임)
    private void UpdateTaskUI(int index)
    {
        UITask cshUITask = taskListContent.GetChild(index).GetComponent<UITask>();
        cshUITask.UpdateUI();
    }

    //Task 배열 반환
    public List<Task> GetTasks()
    {
        return tasks;
    }
    // Task 배열 리셋
    public void ResetTasks()
    {
        isAddedAllTasks = false;
        tasks.Clear();
    }
    // 모든 Task들을 추가했음을 설정
    public void AddAllTasks()
    {
        isAddedAllTasks = true;
    }
    // 모든 Task가 추가되었는지 여부를 반환
    public bool IsAddedAllTasks() 
    {
        return isAddedAllTasks;
    }
    // 모든 Task들의 완료 여부를 반환
    public bool IsCompleteAllTasks()
    {
        // 모든 Task가 추가된 경우에만 확인함
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
//    private string title;      // 제목
//    private string description; // 설명
//    private bool isCompleted;   // 완료되었는지

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