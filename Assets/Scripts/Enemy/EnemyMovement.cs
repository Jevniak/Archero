using System;
using System.Collections;
using System.Security.Principal;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField, Header("Дальность передвижения")]
        private float maxPositionMovement;

        [SerializeField, Header("Время неподвижности")]
        private float timeStuck;

        [SerializeField, Header("Скорость передвижения")]
        private float speed;

        private bool startWaiting;
        
        private EnemyAttack _enemyAttack;

        // получаем игрока
        private Transform target;

        // может ли двигаться
        private bool canMove;

        // начальная позиция
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 moveToPosition;

        private void Awake()
        {
            startPosition = transform.position;
            moveToPosition = startPosition;
        }

        private void Start()
        {
            target = GameObject.FindWithTag("Player").transform;
            
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        private void Update()
        {
            if (canMove && Vector3.Distance(transform.position, target.position) < 10)
            {
                moveToPosition = new Vector3(startPosition.x + Random.Range(-maxPositionMovement, maxPositionMovement),
                    startPosition.y, startPosition.z + Random.Range(-maxPositionMovement, maxPositionMovement));
                canMove = false;
            }

            if (target != null && Vector3.Distance(transform.position, target.position) >= 10)
            {
                _enemyAttack.attacking = false;
            }
            else
            {
                Rotate();
                _enemyAttack.attacking = true;
            }

            Move();
        }

        private void Move()
        {
            if (transform.position != moveToPosition)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);
            }
            else if (!startWaiting)
            {
                StartCoroutine(WaitStuck());
                startWaiting = true;
            }
        }

        private void Rotate()
        {
            // поворачиваем на игрока
            transform.LookAt(target);
        }

        private IEnumerator WaitStuck()
        {
            canMove = false;
            yield return new WaitForSeconds(timeStuck);
            canMove = true;
            startWaiting = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                //останавливаем если встали в стену
                moveToPosition = startPosition;
                StartCoroutine(WaitStuck());
            }
        }
    }
}