using Character.CharactersDart;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController2D controller;
        [SerializeField] private float runSpeed = 40f;
        [SerializeField] private AnimationParameters animParam;
        [SerializeField] private HidenWeapon hidenWeapon;
        private float _horizontalMove = 0f;
        private bool _crouch = false;
        private bool _jump = false;

        void Update()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animParam.SetFloatParameter("Speed", Mathf.Abs(_horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
                animParam.SetBoolParameter("IsJumping" , true);
                animParam.SetBoolParameter("IsCrouch", false);
                _crouch = false;
            }
            if (Input.GetButtonDown("Crouch"))
            {
                animParam.SetBoolParameter("IsCrouch", !_crouch);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                _crouch = !_crouch;
            }
            if (Input.GetButtonDown("Fire1")&& _crouch && hidenWeapon.ShootPermission())
            {
                animParam.SetBoolParameter("IsShooting", true);
                hidenWeapon.Shoot();
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                animParam.SetBoolParameter("IsShooting", false);
            }
        }
        public void OnLanding()
        {
            animParam.SetBoolParameter("IsJumping", false);
        }
        private void FixedUpdate()
        {
            controller.Move(_horizontalMove* Time.fixedDeltaTime,_crouch,_jump);
            _jump = false;
        }
    }
}
