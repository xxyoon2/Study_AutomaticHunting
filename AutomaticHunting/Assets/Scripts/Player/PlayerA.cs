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

public class PlayerA : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _cooltimeBar;
    [SerializeField] private TextMeshProUGUI _damageText;

    private IEnumerator _attack = null;

    private PState state = PState.Nomal;

    Player player = new Player();

    private void Start()
    {
        _attack = Attack();
        StartCoroutine(_attack);

        GameManager.Instance.EndGameEvent.AddListener(StopAction);
        GameManager.Instance.Hitting.AddListener(Hit);
    }

    private void OnDisable()
    {
        GameManager.Instance.EndGameEvent.RemoveListener(StopAction);
        GameManager.Instance.Hitting.RemoveListener(Hit);
    }

    /// <summary>
    /// 공격
    /// </summary>
    IEnumerator Attack()
    {
        _cooltimeBar.maxValue = 1f + player.Speed;
        float actionTime = 0f;
        WaitForSeconds attackActionTime = new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForSeconds(1f);
            actionTime += 1f;
            _cooltimeBar.value = actionTime;

            if (actionTime >= _cooltimeBar.maxValue)
            {
                state = PState.Attack;
                Debug.Log($"{gameObject} : 죽어랏!!!!~!~!");
                GameManager.Instance.HitOtherPlayer(player.StrikingPower);


                yield return attackActionTime;

                state = PState.Nomal;
                actionTime = 0f;
                _cooltimeBar.value = 0f;

            }
        }
    }

    /// <summary>
    /// 맞음
    /// </summary>
    /// <param name="damage"></param>
    private void Hit(int damage)
    {
        if (state == PState.Attack)
        {
            return;
        }

        player.Hit(damage);

        if (player.HealthPoint <= 0)
        {
            GameManager.Instance.BattleEnd();
            Debug.Log($"{gameObject} : 컥컥...");
        }

        _damageText.text = "-" + damage;
        _hpBar.value = player.HealthPoint;
    }

    /// <summary>
    /// 코루틴 중단
    /// </summary>
    private void StopAction()
    {
        StopCoroutine(_attack);
    }
}
