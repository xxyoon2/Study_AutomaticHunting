using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int HealthPoint { get; private set; }
    public int currentHP { get; private set; }
    public int StrikingPower { get; private set; }
    public int DefensivePower { get; private set; }

    public int Speed { get; private set; }

    public Player()
    {
        HealthPoint = 100;
        currentHP = HealthPoint;
        StrikingPower = 10;
        DefensivePower = 5;
        Speed = Random.Range(0, 5);
    }

    public Player(int hp, int atk, int def)
    {
        HealthPoint = hp;
        currentHP = HealthPoint;
        StrikingPower = atk;
        DefensivePower = def;
        Speed = Random.Range(0, 5);
    }

    public void Heal(int health)
    {
        currentHP += health;
    }

    public void Hit(int damage)
    {
        currentHP -= (damage - DefensivePower);
    }
}