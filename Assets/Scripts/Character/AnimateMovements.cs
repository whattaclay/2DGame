using Character.Attacks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    [RequireComponent(typeof(CharacterController2D))]
    public class AnimateMovements : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private bool _hitSwitcher;
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");
        private static readonly int IsShoot = Animator.StringToHash("IsShoot");
        private static readonly int IsHit = Animator.StringToHash("IsHit");
        private static readonly int HitSwitcher = Animator.StringToHash("HitSwitcher");
        private static readonly int IsPowerFullHit = Animator.StringToHash("IsPowerFullHit");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        public void IsHurt()
        {
            animator.SetTrigger(Hurt);
        }
        private void Update()
        {
            switch (CharacterController2D.FireState)
            {
                case FireState.DistanceAttack:
                    animator.SetTrigger(IsShoot);
                    CharacterController2D.FireState = FireState.None;
                    break;
                case FireState.MagicHit:
                    animator.SetTrigger(IsHit);
                    CharacterController2D.FireState = FireState.None;
                    _hitSwitcher = RandomBool();
                    break;
                case FireState.PowerFullMagicHit:
                    animator.SetTrigger(IsPowerFullHit);
                    CharacterController2D.FireState = FireState.None;
                    break;
                default:
                    CharacterController2D.FireState = FireState.None;
                    break;
            }
            animator.SetBool(HitSwitcher, _hitSwitcher);
            animator.SetBool(IsFalling, CharacterController2D.MoveState == MoveState.Fall);
            animator.SetBool(IsJumping, CharacterController2D.MoveState == MoveState.Jump);
            animator.SetBool(IsMoving,CharacterController2D.MoveState == MoveState.Move);
            animator.SetBool(IsDead, CharacterController2D.MoveState == MoveState.Dead);
            if (CharacterController2D.MoveState == MoveState.Dead)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        
        public void IsCrouching(bool isCrouching)
        {
            animator.SetBool(IsCrouch, isCrouching);
        }
        private static bool RandomBool()
        {
            var rand = Random.Range(0f, 10f);
            var randBool = (rand > 5);
            return randBool;
        }
    }
}