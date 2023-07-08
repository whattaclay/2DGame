using UnityEngine;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController2D controller;
        [SerializeField] private float runSpeed = 40f;
        private float _horizontalMove;
        private bool _isCrouch;
        private bool _isStillCrouching;
        private bool _isJump;
        void Update()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; 
            if (Input.GetButtonDown("Jump") && !_isStillCrouching) 
            {
                _isJump = true;
                _isCrouch = false;
            }
            _isCrouch = Input.GetButton("Crouch");
        }
        public void OnCrouching(bool isCrouching)
        {
            _isStillCrouching = isCrouching;
        }
        public void OnLanding() 
        {
            _isJump = false;
        }
        
        private void FixedUpdate()
        {
            controller.Move((_horizontalMove * Time.fixedDeltaTime), _isCrouch, _isJump); 
        }
    }
}
