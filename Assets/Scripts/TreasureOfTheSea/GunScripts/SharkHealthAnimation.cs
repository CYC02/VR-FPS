using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkHealthAnimation : MonoBehaviour
{
    private Animator _animator;
    public FishHealthAttribute _enemy;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (_enemy != null)
        {
            if (_enemy.health < 50)
            {
                _animator.SetBool("healthLessThan50", true);
            }
            if (_enemy.health == 0)
            {
                _animator.SetBool("healthEmpty", true);
                Destroy(_enemy);
            }
        }
    }
}
