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

    [Header("Data")]
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _strength = 2;
    [SerializeField] private float _speed;

    private IEnumerator _attack = null;

    private PState state = PState.Nomal;

    private void Start()
    {
        _speed = GameManager.Instance.SpeedDistribution();

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

    IEnumerator Attack()
    {
        _cooltimeBar.maxValue = 1f + _speed;
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
                GameManager.Instance.HitOtherPlayer(_strength);


                yield return attackActionTime;

                state = PState.Nomal;
                actionTime = 0f;
                _cooltimeBar.value = 0f;

            }
        }
    }

    private void Hit(int damage)
    {
        if (state == PState.Attack)
        {
            return;
        }

        _hp -= damage;

        if (_hp <= 0)
        {
            _hp = 0;
            GameManager.Instance.BattleEnd();
            Debug.Log($"{gameObject} : 컥컥...");
        }

        _damageText.text = "-" + damage;
        _hpBar.value = _hp;
    }

    private void StopAction()
    {
        StopCoroutine(_attack);
    }
}
