using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    private float _horizontalMove = 0f;
    [SerializeField] private float runSpeed = 40f;
    private bool _jump = false;
    private bool _crouch = false;
    [SerializeField] private Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");

    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            animator.SetBool(IsJumping , true);
            animator.SetBool(IsCrouch, false);
            _crouch = false;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool(IsCrouch, !_crouch);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _crouch = !_crouch;
        }
    }
    public void OnLanding()
    {
        animator.SetBool(IsJumping, false);
    }
    private void FixedUpdate()
    {
        controller.Move(_horizontalMove* Time.fixedDeltaTime,_crouch,_jump);
        _jump = false;
    }
}
