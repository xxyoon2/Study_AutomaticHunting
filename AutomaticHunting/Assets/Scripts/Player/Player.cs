using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int HealthPoint { get; private set; }
    public int StrikingPower { get; private set; }
    public int DefensivePower { get; private set; }

    public int Speed { get; private set; }

    public Player()
    {
        HealthPoint = 100;
        StrikingPower = 10;
        DefensivePower = 5;
        Speed = Random.Range(0, 5);
    }

    public Player(int hp, int atk, int def)
    {
        HealthPoint = hp;
        StrikingPower = atk;
        DefensivePower = def;
        Speed = Random.Range(0, 5);
    }

    public void Hit(int damage)
    {
        HealthPoint -= (damage - DefensivePower);
    }

    public void Heal(int health)
    {
        HealthPoint += health;
    }
}