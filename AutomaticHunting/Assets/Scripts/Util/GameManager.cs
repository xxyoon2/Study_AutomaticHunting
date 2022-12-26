using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent EndGameEvent = new UnityEvent();
    public UnityEvent<int> Hitting = new UnityEvent<int>();

    /// <summary>
    /// 속도 랜덤으로 지정해줌
    /// </summary>
    /// <returns>속도</returns>
    public float SpeedDistribution()
    {
        return Random.Range(1, 5);
    }

    /// <summary>
    /// 사냥 끝남을 알림
    /// </summary>
    public void BattleEnd()
    {
        EndGameEvent.Invoke();
    }

    /// <summary>
    /// 다른 플레이어에게 공격함을 알림
    /// </summary>
    /// <param name="damage"></param>
    public void HitOtherPlayer(int damage)
    {
        Hitting.Invoke(damage);
    }
}
