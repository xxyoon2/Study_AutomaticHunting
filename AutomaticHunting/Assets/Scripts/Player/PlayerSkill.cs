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

    private void A(Player player)
    {
        // 최대 체력의 20%를 회복(턴 3)
        int health = Mathf.RoundToInt(player.HealthPoint * 20 / 100);
        player.Heal(health);
    }

    private void B(Player player)
    {
        // 기본 데미지 50 + 자신의 방어력 10%만큼 피해를 입힘(턴 3)
    }

    private void C(Player player)
    {
        // 공격력의 130%의 피해를 입힘(턴 2)
    }

    private void D(Player player)
    {
        // 공격력의 90%의 피해를 두 번 입힘(턴 4)
    }
}
