using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour, IAttackeble<int>
    {
        [SerializeField, Header("Наносимый урон")]
        private int attackDamage;

        [SerializeField, Header("Скорость стрельбы")]
        private float attackCooldown;

        [SerializeField, Header("Скорость полета выстрела")]
        private float attackForcePower;

        [SerializeField] private Transform bulletStartPosition;

        // префаб пули
        [SerializeField] private GameObject bulletPrefab;

        private bool canAttack = true;
        public bool attacking;

        private void Update()
        {
            if (attacking && canAttack)
            {
                Attack(attackDamage);
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
            GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition.position, Quaternion.Euler(0, 0, 90));
            // даем урона пуле
            bullet.GetComponent<Bullet>().damage = damageGiven;
            //запускаем пулю
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * attackForcePower, ForceMode.Impulse);
        }

        private IEnumerator CooldownAttack()
        {
            // задержка перед атакой
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}