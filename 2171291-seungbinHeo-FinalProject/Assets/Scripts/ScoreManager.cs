using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 100; // ���� ���ھ�
    private List<string> mistakes = new List<string>(); // �Ǽ� �׸�

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

    // �Ǽ� �߰� �� ���� ����
    public void AddMistake(string mistake)
    {
        mistakes.Add(mistake);
        score -= 10; // �Ǽ����� 10�� ����, �ʿ信 ���� ����
    }
    // ���� ������
    public int GetScore()
    {
        return score;
    }
    // �Ǽ� �׸� ������
    public List<string> GetMistakes()
    {
        return mistakes;
    }
    // ���� �� �Ǽ� �׸� �ʱ�ȭ
    public void ResetScore()
    {
        score = 100;
        mistakes.Clear();
    }
}
