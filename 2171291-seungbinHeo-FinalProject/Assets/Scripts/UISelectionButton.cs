using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ȳ ���� ��ư�� ����� �޼����
public class UISelectionButton : MonoBehaviour
{
    public void SelectAccidentSituation()
    {
        GameManager.Instance.SelectSituation(Situation.Accident);
    }
    public void SelectWinterSituation()
    {
        GameManager.Instance.SelectSituation(Situation.Winter);
    }
    public void SelectBreakdownSituation()
    {
        GameManager.Instance.SelectSituation(Situation.Breakdown);
    }
}
