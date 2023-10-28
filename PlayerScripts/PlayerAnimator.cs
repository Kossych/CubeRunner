using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public bool IsJumping
    {
        set => _animator.SetBool("Jump", value);
    }

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DisableAnimation()
    {
        _animator.enabled = false;
    }
}
