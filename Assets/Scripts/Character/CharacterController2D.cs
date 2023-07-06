using System;
using Character.CharactersMagicBall;
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

		private const float GroundedCircleRadius = 0.1f;
		private bool _isGrounded;
		private const float CeilingCircleRadius = 0.1f;
		private Rigidbody2D _rigidbody2D;
		private bool _facingRight = true;
		private Vector3 _velocity = Vector3.zero;
		private bool _isCrouching;
		public MoveState moveState;
		public FireState fireState;
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
		public void Move(float move, bool crouch, bool jump)
		{
			if (!crouch) 
			{
				if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingCircleRadius, whatIsGround))
				{
					crouch = true;
				}
			}
			if (_rigidbody2D.velocity.y < -0.8f)
			{
				moveState = MoveState.Fall;
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
				if (Input.GetButton("Fire1"))
				{
					distanceAttack.Shoot();
				}
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
				case > 0 when !_facingRight:  // если игрок повернут налево во время движения вправо, переворачиваем его
				case < 0 when _facingRight:  // если игрок повернут направо во время движения влево, переворачиваем его
					Flip();
					break;
			}
			if (Math.Abs(move) > 0 && !jump && _rigidbody2D.velocity.y >= 0)
			{
				moveState = MoveState.Move;
			}
			else if(Math.Abs(move) == 0f && !jump && _rigidbody2D.velocity.y >= 0)
			{
				moveState = MoveState.Idle;
			}
			if (!_isGrounded || !jump || crouch) return; 
			_isGrounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			moveState = MoveState.Jump;
		}
		private void Flip()
		{
			_facingRight = !_facingRight;
			transform.Rotate(0f,180f,0f);
		}
	}
}
