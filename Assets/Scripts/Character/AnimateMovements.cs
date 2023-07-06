using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController2D))]
    public class AnimateMovements : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float shootAnimationTime = 0.5f;
        private CharacterController2D _characterController2D;
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");
        private static readonly int IsShooting = Animator.StringToHash("IsShooting");
        private void Awake()
        {
            _characterController2D = GetComponent<CharacterController2D>();
        }
        private void Update()
        {
            if (_characterController2D.fireState == FireState.Fire1)
            {
                StartCoroutine(ShootAnimationTime());
            }
            animator.SetBool(IsShooting, _characterController2D.fireState == FireState.Fire1);
            animator.SetBool(IsFalling, _characterController2D.moveState == MoveState.Fall);
            animator.SetBool(IsJumping, _characterController2D.moveState == MoveState.Jump);
            animator.SetBool(IsMoving,_characterController2D.moveState == MoveState.Move);
        }
        public void IsCrouching(bool isCrouching)
        {
            animator.SetBool(IsCrouch, isCrouching);
        }
        private IEnumerator ShootAnimationTime()
        {
            yield return new WaitForSeconds(shootAnimationTime);
            _characterController2D.fireState = FireState.None;
        }
    }
}