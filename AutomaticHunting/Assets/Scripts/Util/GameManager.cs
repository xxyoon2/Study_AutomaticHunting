using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehaviour<GameManager>
{
    public UnityEvent EndGameEvent = new UnityEvent();
    public UnityEvent<int> Hitting = new UnityEvent<int>();

    public float SpeedDistribution()
    {
        return Random.Range(1, 5);
    }

    public void BattleEnd()
    {
        EndGameEvent.Invoke();
    }

    public void HitOtherPlayer(int damage)
    {
        Hitting.Invoke(damage);
    }
}
