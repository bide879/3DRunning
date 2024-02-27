using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Player Player
    {
        get
        {
            if(player == null)  // 초기화 전에 Player에 접근했을 경우
            {
                OnInitialize();
            }
            return player;
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindAnyObjectByType<Player>();
    }

    bool isSpeedUp = false;

    public Action onSpeedUp;
    public void SpeedUp()
    {
        if (!isSpeedUp)
        {
            onSpeedUp?.Invoke();
            isSpeedUp = true;
            Debug.Log("게임매니저 SpeedUp");
        }
        else
        {
           
        }
    }

    public Action onSpeedUpEnd;
    public void SpeedUpEnd()
    {
        if(isSpeedUp)
        {
            onSpeedUpEnd?.Invoke();
            isSpeedUp = false;
            Debug.Log("게임매니저 SpeedUpEnd");
        }
    }

}
