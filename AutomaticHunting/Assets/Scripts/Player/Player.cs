using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _speed;

    private IEnumerator _attack = null;
    private WaitForSeconds _actionTime = new WaitForSeconds(3.0f);

    private void Start()
    {
        _attack = Attack();
        StartCoroutine(_attack);
    }

    IEnumerator Attack()
    {
        while(true)
        {
            yield return _actionTime;
            Debug.Log("죽어랏!!!!~!~!");
        }
    }

    private void Hit()
    {
        _hp -= 1;
    }
}
