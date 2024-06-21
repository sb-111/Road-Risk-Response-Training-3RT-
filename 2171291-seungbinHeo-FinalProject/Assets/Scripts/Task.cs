using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Task
{
    private string title;       // 제목
    private string description; // 설명
    private bool isCompleted;   // 완수했는지
    private bool isTimeOver;    // 제한 시간 초과 여부

    //private bool isTryBad;      // 잘못된 행동 트라이했는지
    private int score;          // 해당 Task의 점수
    private string mistake;     // 실수 내용
    private float timeLimit;    // 제한시간
    private float startTime;
    //public bool isFailed;

    public Task(string title, string description, int score, string mistake, float timeLimit)
    {
        this.title = title;
        this.description = description;
        this.score = score;
        this.mistake = mistake;
        this.timeLimit = timeLimit;
        isCompleted = false;
        isTimeOver = false;
        startTime = Time.time;
        //isTryBad = false;
    }
    public void Complete()
    {
        //isCompleted = true;
        if (Time.time - startTime <= timeLimit) // 제한시간 내 성공 시
        {
            isCompleted = true;
        }
        else // 제한시간 내 성공하지 못할 시
        {
            isTimeOver = true;
        }
    }
    public void TimeOver()
    {
        isTimeOver = true;
    }
    //public void TryBad()
    //{
    //    isTryBad = true;
    //}
    public bool IsComplete()
    {
        return isCompleted;
    }
    public bool IsTimeOver()
    {
        return isTimeOver;
    }
    //public bool IsTryBad()
    //{
    //    return isTryBad;
    //}
    public string GetTitle()
    {
        return title;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetScore()
    {
        return score;
    }
    public string GetMistake()
    {
        return mistake;
    }
    public float GetTimeLimit()
    {
        return timeLimit;
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0, timeLimit - (Time.time - startTime));
    }

    public float GetStartTime()
    {
        return startTime;
    }
}