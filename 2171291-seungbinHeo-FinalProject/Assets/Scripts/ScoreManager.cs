using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 100; // 총점 스코어
    private List<string> mistakes = new List<string>(); // 실수 항목

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

    // 실수 추가 및 점수 감점
    public void AddMistake(string mistake)
    {
        mistakes.Add(mistake);
        score -= 10; // 실수마다 10점 감점, 필요에 따라 조정
    }
    // 점수 얻어오기
    public int GetScore()
    {
        return score;
    }
    // 실수 항목 얻어오기
    public List<string> GetMistakes()
    {
        return mistakes;
    }
    // 점수 및 실수 항목 초기화
    public void ResetScore()
    {
        score = 100;
        mistakes.Clear();
    }
}
