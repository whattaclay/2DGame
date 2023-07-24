using System;
using Character.Attacks;
using Character.Attacks.DistanceAttack;
using Character.Attacks.MagicHit;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
	public class CharacterController2D : MonoBehaviour
	{
		[SerializeField] private float jumpForce = 400f;
		[Range(0, 1)] [SerializeField] private float crouchSpeed;
		[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
		[Header("Слои, являющиеся окружением")]
		[SerializeField] private LayerMask whatIsGround;
		[SerializeField] private Transform groundCheck;
		[SerializeField] private Transform ceilingCheck;
		[Header("Выключение коллайдера при ползке")]
		[SerializeField] private Collider2D crouchDisableCollider;
		[Header("Выстрел магией")]
		[SerializeField] private DistanceAttack distanceAttack;
		[Header("Удар магией")]
		[SerializeField] private MagicHit magicHit;

		private Rigidbody2D _rigidbody2D;
		private float _flipValue = 1f; // 1 - игрок повернут вправо, -1 - влево
		private Vector3 _velocity = Vector3.zero;
		private const float GroundedCircleRadius = 0.1f;
		private const float CeilingCircleRadius = 0.1f;
		private const float FallingConst = -1.5f;
		private bool _isGrounded;
		private bool _isCrouching;
		public static MoveState MoveState;
		public static FireState FireState;
		[Header("Events")]
		[Space]
		public UnityEvent onLandEvent;
		public UnityEvent<bool> onCrouchEvent;

		private void Awake() 
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			onLandEvent ??= new UnityEvent(); 
			onCrouchEvent ??= new UnityEvent<bool>();
		}
		private void FixedUpdate()
		{
			bool isGrounded = _isGrounded;
			_isGrounded = false;
			var hasCollider = Physics2D.OverlapCircle(groundCheck.position, GroundedCircleRadius, whatIsGround);
			if (!hasCollider) return;
			_isGrounded = true;
			if (!isGrounded)
				onLandEvent.Invoke();
		}

		private void Update()
		{
			if (_isCrouching || !_isGrounded) return;
			if (Input.GetButton("Fire1") && MoveState != MoveState.Move)
			{
				distanceAttack.Shoot();
			}
			if (Input.GetButtonDown("Fire2"))
			{
				magicHit.Hit();
			}
			if (Input.GetButtonDown("Fire3")&& MoveState != MoveState.Move)
			{
				magicHit.PowerFullHit();
			}
		}
		public void Move(float move, bool crouch, bool jump)
		{
			if (!crouch) 
			{
				if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingCircleRadius, whatIsGround))
				{
					crouch = true;
				}
			}
			if (_rigidbody2D.velocity.y < FallingConst)
			{
				MoveState = MoveState.Fall;
			}
			if (crouch)
			{
				if (!_isCrouching)
				{
					_isCrouching = true;
					onCrouchEvent.Invoke(true);
				}
				move *= crouchSpeed; 
				crouchDisableCollider.enabled = false;
			} else
			{
				crouchDisableCollider.enabled = true;
				if (_isCrouching)
				{
					_isCrouching = false;
					onCrouchEvent.Invoke(false);
				}
			}
			var velocity = _rigidbody2D.velocity;
			Vector3 targetVelocity = new Vector2(move * 10f, velocity.y);
			_rigidbody2D.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);
			switch (move)
			{
				case > 0 when _flipValue < 0 :  // если игрок повернут налево во время движения вправо, переворачиваем его
				case < 0 when _flipValue > 0:  // если игрок повернут направо во время движения влево, переворачиваем его
					_flipValue = ChangeDirectionView.Flip(transform, _flipValue);
					break;
			}
			if (Math.Abs(move) > 0 && !jump && _rigidbody2D.velocity.y >= 0)
			{
				MoveState = MoveState.Move;
			}
			else if(Math.Abs(move) == 0f && !jump && _rigidbody2D.velocity.y >= 0)
			{
				MoveState = MoveState.Idle;
			}
			if (!_isGrounded || !jump || crouch) return; 
			_isGrounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			MoveState = MoveState.Jump;
		}
	}
}
