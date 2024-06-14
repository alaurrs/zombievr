using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Animator _animator;

    private static readonly int IsAttacked = Animator.StringToHash("IsAttacked");
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger(IsAttacked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
