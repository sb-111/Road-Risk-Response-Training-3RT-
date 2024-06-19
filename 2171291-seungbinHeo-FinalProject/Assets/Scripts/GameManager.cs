using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UITimer uiTimer;
    public UIFinal uiFinal;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartTimer()
    {
        uiTimer.gameObject.SetActive(true); // uiTimer를 가진 오브젝트 활성화
        if (uiTimer != null)
        {
            uiTimer.StartTimer();
        }
    }
    // called by UITimer
    public void FinishGame()
    {
        // uiFinal을 이용해 결과창을 보여줌
    }
    //public void StopTimer()
    //{
    //    if (uiTimer != null)
    //    {
    //        uiTimer.StopTimer();
    //    }
    //}

}
