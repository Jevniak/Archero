using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerAttack : MonoBehaviour, IAttackeble<int>
    {
        // задержка между атаками
        [SerializeField] private float attackCooldown = 1;
        // наносимый урон
        [SerializeField] private int damage;
        // позиция начала полета пули
        [SerializeField] private Transform bulletStartPosition;
        // префаб пули
        [SerializeField] private GameObject bulletPrefab;
        // может ли игрок атаковать (зависит от задержки атаки)
        private bool canAttack = true;
        // разрешено ли атаковать (стоим ли на месте)
        public bool attacking { private get; set; }
        // список всех врагов
        public List<GameObject> enemies { get; private set; }

        // текущая ближайшая цель
        [SerializeField] private Transform currentTarget;


        private void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        private void Update()
        {
            if (attacking)
            {
                FindNearestEnemies();
                if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) < 10f)
                {
                    transform.LookAt(currentTarget);
                    if (canAttack)
                        Attack(damage);    
                }
                
            }
        }

        private void FindNearestEnemies()
        {
            // находим ближайшего врага из списка врагов
            if (enemies.Count > 0)
            {
                float distance = Mathf.Infinity;
                Vector3 position = transform.position;
                foreach (GameObject enemy in enemies)
                {
                    if (enemy != null)
                    {
                        Vector3 diff = enemy.transform.position - position;
                        float currentDistance = diff.sqrMagnitude;
                        if (currentDistance < distance)
                        {
                            currentTarget = enemy.transform;
                            distance = currentDistance;
                        }
                    }
                    else
                    {
                        enemies.Remove(enemy);
                        break;
                    }
                }
            }
        }

        public void Attack(int damageGiven)
        {
            // производим атаку
            // запрещаем атаковать (я вам запрещаю атаковать)
            canAttack = false;
            // запускаем задержку перед атакой
            StartCoroutine(CooldownAttack());
            // создаем пулю
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition.position, Quaternion.Euler(0,0,90));
            // даем урона пуле
            bullet.GetComponent<Bullet>().damage = damageGiven;
            //запускаем пулю
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        }

        private IEnumerator CooldownAttack()
        {
            // задержка перед атакой
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}