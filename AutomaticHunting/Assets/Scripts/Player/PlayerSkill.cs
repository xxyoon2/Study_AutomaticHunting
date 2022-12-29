using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillList
{
    None,
    A,
    B,
    C,
    D, 
    Max,
}

public class PlayerSkill : MonoBehaviour
{
    private int[] _skillCoolTime = { 0, 3, 3, 2, 4 , 0};

    public void UseSkill(Player player, SkillList skill)
    {
        switch(skill)
        {
            case SkillList.A:
                A(player);
                break;
            case SkillList.B:
                B(player);
                break;
            case SkillList.C:
                C(player);
                break;
            case SkillList.D:
                D(player);
                break;
            default:
                // 일반공격;
                break;
        }
    }

    public int GetCoolTime(SkillList skill)
    {
        return _skillCoolTime[(int)skill];
    }

    private void A(Player player)
    {
        // 최대 체력의 20%를 회복(턴 3)
        int health = Mathf.RoundToInt(player.HealthPoint * 20 / 100);
        player.Heal(health);
    }

    private void B(Player player)
    {
        // 기본 데미지 50 + 자신의 방어력 10%만큼 피해를 입힘(턴 3)
        int damage = 50 + Mathf.RoundToInt(player.DefensivePower * 10 / 100);
        GameManager.Instance.HitOtherPlayer(damage);
    }

    private void C(Player player)
    {
        // 공격력의 130%의 피해를 입힘(턴 2)
        int damage = player.StrikingPower + Mathf.RoundToInt(player.StrikingPower * 30 / 100);
        GameManager.Instance.HitOtherPlayer(damage);
    }

    private void D(Player player)
    {
        // 공격력의 90%의 피해를 두 번 입힘(턴 4)
        int damage = Mathf.RoundToInt(player.StrikingPower * 90 / 100);
        for (int i = 0; i < 2; ++i)
        {
            GameManager.Instance.HitOtherPlayer(damage);
        }
    }
}
