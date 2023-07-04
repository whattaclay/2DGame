using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Character
{
	public class CharacterController2D : MonoBehaviour
	{
		[Header("Сила прыжка")]
		[SerializeField] private float jumpForce = 400f;
		[Header("Скорость при ползке")]
		[Range(0, 1)] [SerializeField] private float crouchSpeed = 0f;
		[Header("Плавность движений")]
		[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
		[Header("Контроль персонажа в воздухе")]
		[SerializeField] private bool airControl = false;	
		[Header("Слои, являющиеся окружением")]
		[SerializeField] private LayerMask whatIsGround;
		[Header("Точка проверки земли")]
		[SerializeField] private Transform groundCheck;
		[Header("Точка проверки потолка")]
		[SerializeField] private Transform ceilingCheck;
		[Header("Выключение коллайдера при ползке")]
		[SerializeField] private Collider2D crouchDisableCollider;

		private const float GroundedCircleRadius = 0.1f;
		private bool _isGrounded;
		private const float CeilingCircleRadius = 0.1f;
		private Rigidbody2D _rigidbody2D;
		private bool _facingRight = true;
		private Vector3 _velocity = Vector3.zero;
	
		[Header("Events")]
		[Space]

		public UnityEvent onLandEvent;

		[System.Serializable]
		public class BoolEvent : UnityEvent<bool> { } 
		[FormerlySerializedAs("OnCrouchEvent")] public BoolEvent onCrouchEvent;
		private bool _isCrouching = false;

		private void Awake() //создаем булевый и юнитевский ивент и берем рб с персонажа
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			onLandEvent ??= new UnityEvent(); //если ивент равен нулю, добавляяем его
			onCrouchEvent ??= new BoolEvent();
		}

		private void FixedUpdate()
		{
			bool isGrounded = _isGrounded;
			_isGrounded = false;
			//плеер на земле, если коллайдер окружности сталкивается с окружением, задаваемом слоями
			var hasCollider = Physics2D.OverlapCircle(groundCheck.position, GroundedCircleRadius, whatIsGround);
			if (!hasCollider) return;
			_isGrounded = true;
			if (!isGrounded)
				onLandEvent.Invoke();
		}
		public void Move(float move, bool crouch, bool jump)
		{
			if (!crouch) //проверяем есть ли над головой препятствие во время отжатия клавиши ползка
			{
				// если есть препятствие, то продолжаем ползать
				if (Physics2D.OverlapCircle(ceilingCheck.position, CeilingCircleRadius, whatIsGround))
				{
					crouch = true;
				}
			}
			if (_isGrounded || airControl) //контроль персонажа(условие, чтобы можно было выключать контроль персонажа в воздухе)
			{
				// если ползаем
				if (crouch)
				{
					if (!_isCrouching)//включаем ивент
					{
						_isCrouching = true;
						onCrouchEvent.Invoke(true);
					}
					move *= crouchSpeed; //уменьшаем скорость персонажа при ползке
					crouchDisableCollider.enabled = false;// отключаем верхний коллайдер
				} else
				{
					crouchDisableCollider.enabled = true;// включаем верхний коллайдер, если не ползаем
					if (_isCrouching)
					{
						_isCrouching = false;
						onCrouchEvent.Invoke(false);//выключаем ивент
					}
				}
				// скорость персонажа, которую собираемся достичь
				Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
				// сглаживаем скорость и добавляем к персонажу
				_rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmoothing);
				switch (move)
				{
					case > 0 when !_facingRight:  // если игрок повернут налево во время движения вправо, переворачиваем его
					case < 0 when _facingRight:  // если игрок повернут направо во время движения влево, переворачиваем его
						Flip();
						break;
				}
			}
			if (!_isGrounded || !jump) return; //при нажатии клавиши прыжка, добавляем силу по оси игрик
			_isGrounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, jumpForce));
		}
		private void Flip()
		{
			_facingRight = !_facingRight;
			transform.Rotate(0f,180f,0f);//поворачиваем персонажа на 180
		}
	}
}
