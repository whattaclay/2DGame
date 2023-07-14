using System.Collections;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAI : MonoBehaviour
    {
        private enum WanderType
        {
            PointToPoint,
            WallToWall
        }
        [SerializeField] private WanderType wanderType;
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float pointsRadius;
        [SerializeField] private float wanderStateSpeed;
        [SerializeField] private float attackStateSpeed;
        [SerializeField] private float attackDelay = 0.9f;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask environmentLayer;
        [SerializeField] private float rayDistance = 3f; //дистанция рейкаста, для поиска игрока(позади ишрока в 3 раза меньше)
        private const float DistanceToAttack = 0.9f;
        private Enemy _enemy;
        private Rigidbody2D _rb;
        private Transform _targetPoint;
        private bool _lostPlayer = true;
        private Vector2 _target;
        private float _flipValue = 1f;
        private Vector3 _currentPosition;
        private bool _isAttack;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _rb = GetComponent<Rigidbody2D>();
            _enemy.enemyState = EnemyState.Walk;
            if (wanderType == WanderType.WallToWall) return; 
            _targetPoint = pointB.transform;
        }
        private void Update()
        {
            if(_rb.bodyType == RigidbodyType2D.Kinematic) return;
            _currentPosition = transform.position;
            if (_lostPlayer) //если игрок не обнаружен, то выполняются стандартные Wander методы
            {
                var velY =_rb.velocity.y;
                _rb.velocity = new Vector2(wanderStateSpeed * _flipValue, velY);
                if (wanderType == WanderType.PointToPoint && _enemy.enemyState == EnemyState.Walk)
                {
                    WanderPointToPoint();
                }
                if (wanderType == WanderType.WallToWall && _enemy.enemyState == EnemyState.Walk)
                {
                    WanderWallToWall();
                }
            }
            if (_enemy.enemyState == EnemyState.Run && _enemy.enemyState != EnemyState.Attack && _enemy.enemyState != EnemyState.Hurt) // скорость игрока при обнаружении игрока
            {
                var velY =_rb.velocity.y;
                _rb.velocity = new Vector2(attackStateSpeed * _flipValue, velY);
            }
            var hitToward = Physics2D.Raycast(_currentPosition,new Vector2(_flipValue, 0f),rayDistance,playerLayer);
            var hitBackward= Physics2D.Raycast(_currentPosition,new Vector2(-_flipValue, 0f),rayDistance/3,playerLayer);
            if (hitBackward.collider) //если игрок со спины, то поворачиваемся
            {
                Flip();
            }
            if (hitToward.collider && !_isAttack) //если игрок спереди и не атакуем его
            {
                _lostPlayer = false;
                if (_enemy.enemyState is EnemyState.Walk or EnemyState.Idle)
                    _enemy.enemyState = EnemyState.Run;
                _target = hitToward.collider.transform.position;
            }
            if (Vector2.Distance(_currentPosition,_target)< DistanceToAttack && hitToward.collider) //если до игрока меньше чем дистанчия для атаки и он в поле зрения
            {
                _rb.velocity = Vector2.zero;
                _lostPlayer = true;
                _enemy.enemyState = EnemyState.Idle;
                if (_isAttack) return;
                StartCoroutine(AttackDelay());
                _enemy.enemyState = EnemyState.Attack;
            }
            else if(Vector2.Distance(_currentPosition,_target)< 1f && !hitToward.collider && _enemy.enemyState != EnemyState.Walk)//если игрока не обнаружили за метр до его
            {                                                                                                                                 //прошлой позиции и его нет в поле зрения
                wanderType = WanderType.WallToWall;
                _enemy.enemyState = EnemyState.Walk;
                _lostPlayer = true;
            }
        }
        private void WanderWallToWall() //метод, в котором енеми будет бродить от стенки до стенки
        {
            RaycastHit2D hitWall = Physics2D.Raycast(
                new Vector2(_currentPosition.x, _currentPosition.y + 0.2f),
                new Vector2(1f*_flipValue, 0f),
                0.5f,
                environmentLayer);
            if (hitWall.collider)
            {
                Flip();
            }
        }
        private void WanderPointToPoint()// метод, в котором енеми будет бродить от точки а до б(задается через инспектор)
        {
            if (Vector2.Distance(_currentPosition, _targetPoint.position) < pointsRadius && _targetPoint == pointB.transform)
            {
                _targetPoint = pointA.transform;
                Flip();
            }
            if (Vector2.Distance(_currentPosition, _targetPoint.position) < pointsRadius && _targetPoint == pointA.transform)
            {
                _targetPoint = pointB.transform;
                Flip();
            }
        }
        private void Flip() //переворачивает модельку игрока
        {
            _flipValue *= -1;
            transform.Rotate(0f,180f,0f);
        }
        private IEnumerator AttackDelay()
        {
            _isAttack = true;
            yield return new WaitForSeconds(attackDelay);
            _isAttack = false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_target, pointsRadius);
            if(wanderType == WanderType.WallToWall) return;
            Gizmos.DrawWireSphere(pointA.transform.position, pointsRadius);
            Gizmos.DrawWireSphere(pointB.transform.position, pointsRadius);
        }
    }
}