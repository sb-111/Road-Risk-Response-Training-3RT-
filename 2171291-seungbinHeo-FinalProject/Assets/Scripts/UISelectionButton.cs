using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상황 선택 버튼에 연결된 메서드들
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
