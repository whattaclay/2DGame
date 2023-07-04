using System.Collections;
using Character.CharactersMagicBall;
using UnityEngine;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController2D controller;
        [SerializeField] private DistanceAttack distanceAttack;
        [SerializeField] private Animator animator;
        [SerializeField] private float runSpeed = 40f;
        private const float ShootAnimationDelay = 0.5f;
        private float _horizontalMove = 0f;
        private bool _isCrouch = false;
        private bool _isJump = false;
        private bool _isShooting = false;
                /// <summary>
                /// TODO анимацию и логику падения
                /// </summary>
        void Update()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //задем значение горизонтального перемещения
            animator.SetFloat("Speed", Mathf.Abs(_horizontalMove)); //задаем в аниматор параметр скорости
            if (Input.GetButtonDown("Jump")) //если нажали клавишу прыжка, то задаем параметры в аниматор, что персонаж прыгнул и следовательно
            {                                  //уже не ползет
                _isJump = true;
                animator.SetBool("IsJumping" , true);
                animator.SetBool("IsCrouch", false);
                _isCrouch = false;
            }
            if (Input.GetButtonDown("Crouch")) 
            {
                _isCrouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                _isCrouch = false;
            }
            if (Input.GetButtonDown("Fire1")&& !distanceAttack.IsAttackCd() && !_isCrouch && !_isJump && !_isShooting)// если персонаж нажал клавишу выстрела,
            {                                               //не идет кд после выстрела, персонаж не ползет,не прыгает и не стреляет, то выполняется выстрел
                _isShooting = true;
                animator.SetBool("IsShooting", _isShooting);
                distanceAttack.Shoot();
                StartCoroutine(AnimationDelay());
            }
        }
        private IEnumerator AnimationDelay() //корутина для анимации выстрела, после делея выключает анимацию и устанавливает булевый параметр, что персонаж не стреляет
        {
            yield return new WaitForSeconds(ShootAnimationDelay);
            _isShooting = false;
            animator.SetBool("IsShooting", _isShooting);
        }
        public void OnLanding() //метод прокидывается в ивент, срабатывающий когда персонаж контактирует с коллизией земли. выключаем анимацию прыжка
        {
            animator.SetBool("IsJumping", false);
            _isJump = false;
        }
        public void IsCrouching(bool isCrouching)
        {
            animator.SetBool("IsCrouch", isCrouching);
        }
        private void FixedUpdate()
        {
            controller.Move((_horizontalMove * Time.fixedDeltaTime), _isCrouch, _isJump); //в физический апдейт передаем параметры движения персонажа
        }
    }
}
