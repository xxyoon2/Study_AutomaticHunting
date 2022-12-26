using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 10;
    [SerializeField] private int _speed;

    private void Attack()
    {

    }

    private void Hit()
    {
        _hp -= 1;
    }
}
