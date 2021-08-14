using System;
using System.Collections;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float rotateSpeed = 20;
        [SerializeField] private VariableJoystick _joystick;
        private PlayerAttack _playerAttack;
        private Rigidbody rb;
        private Vector3 joystickDirection;
        
        

        private bool start;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _playerAttack = GetComponent<PlayerAttack>();
            if (_joystick == null)
                _joystick = FindObjectOfType<VariableJoystick>();

            StartCoroutine(WaitingStartGame());
        }

        private IEnumerator WaitingStartGame()
        {
            yield return new WaitForSeconds(3f);
            UISystem.inst.ChangeWindow();
            start = true;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            if (start)
            {
                joystickDirection = new Vector3(_joystick.Horizontal, _joystick.Vertical);
                _playerAttack.attacking = joystickDirection == Vector3.zero;
                Rotate();
            }
        }

        private void Move()
        {
            Vector3 direction = new Vector3(joystickDirection.x * speed, rb.velocity.y, joystickDirection.y * speed);
            rb.velocity = direction;
        }

        public void Rotate()
        {
            if (new Vector2(_joystick.Vertical, _joystick.Horizontal) == Vector2.zero)
                return;

            Vector3 position = transform.position;
            Vector3 targetDirection = new Vector3(position.x + joystickDirection.x, position.y,
                position.z + joystickDirection.y);
            Vector3 lookDirection = targetDirection - position;

            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * rotateSpeed);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Finish") && _playerAttack.enemies.Count == 0)
            {
                start = false;
                UISystem.inst.ChangeWindow(1);
            }
        }
    }
}