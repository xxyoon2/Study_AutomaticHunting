using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum PState
{
    None,
    Nomal,
    Attack,
    Max,
}

public class Player : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _cooltimeBar;
    [SerializeField] private TextMeshProUGUI _damageText;

    [Header("시작할 때 적용되는 플레이어 데이터")]
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _atk = 10;
    [SerializeField] private int _def = 2;

    [SerializeField] private SkillList _skill1 = SkillList.A;
    [SerializeField] private SkillList _skill2 = SkillList.B;

    private int _skill1Colltime;
    private int _skill2Colltime;

    private PlayerStat _stat;
    private PlayerSkill _skill;

    private IEnumerator _updateBehaviorGauge = null;

    private PState _state = PState.Nomal;

    

    private void Start()
    {
        _stat = new PlayerStat(_maxHP, _atk, _def);

        _skill = GetComponent<PlayerSkill>();
        _skill1Colltime = _skill.GetCoolTime(_skill1);
        _skill2Colltime = _skill.GetCoolTime(_skill2);

        _updateBehaviorGauge = UpdateBehaviorGauge();
        StartCoroutine(_updateBehaviorGauge);

        GameManager.Instance.EndGameEvent.AddListener(StopAction);
        GameManager.Instance.Hitting.AddListener(Hit);
    }

    private void OnDisable()
    {
        GameManager.Instance.EndGameEvent.RemoveListener(StopAction);
        GameManager.Instance.Hitting.RemoveListener(Hit);
    }

    /// <summary>
    /// 행동 게이지 업데이트
    /// </summary>
    IEnumerator UpdateBehaviorGauge()
    {
        _cooltimeBar.maxValue = 1f + _stat.Speed;
        float actionTime = 0f;
        WaitForSeconds attackActionTime = new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            actionTime += 1f;
            _cooltimeBar.value = actionTime;

            if (actionTime >= _cooltimeBar.maxValue)
            {
                Attack();

                _state = PState.Nomal;
                actionTime = 0f;
                _cooltimeBar.value = 0f;

            }
        }
    }

    private void Attack()
    {
        _state = PState.Attack;
        _skill1Colltime = _skill2Colltime -= 1;

        if (_skill2Colltime <= 0)
        {
            _skill.UseSkill(_stat, _skill2);
            _skill2Colltime = _skill.GetCoolTime(_skill2);

            return;
        }
        else if (_skill1Colltime <= 0)
        {
            _skill.UseSkill(_stat, _skill1);
            _skill1Colltime = _skill.GetCoolTime(_skill1);

            return;
        }
        else
        {
            GameManager.Instance.HitOtherPlayer(_stat.StrikingPower);
        }
    }

    /// <summary>
    /// 맞음
    /// </summary>
    /// <param name="damage"></param>
    private void Hit(int damage)
    {
        if (_state == PState.Attack)
        {
            return;
        }

        _stat.Hit(damage);

        if (_stat.currentHP <= 0)
        {
            GameManager.Instance.BattleEnd();
            Debug.Log($"{gameObject} : 컥컥...");
        }

        _damageText.text = "-" + damage;
        _hpBar.value = _stat.HealthPoint;
    }

    /// <summary>
    /// 코루틴 중단
    /// </summary>
    private void StopAction()
    {
        StopCoroutine(_updateBehaviorGauge);
    }
}
