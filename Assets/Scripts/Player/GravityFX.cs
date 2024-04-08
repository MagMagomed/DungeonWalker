using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Player
{
    internal class GravityFX : MonoBehaviour
    {

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _gravity;
        [SerializeField] private Vector3 _Velocity;
        private void Update()
        {
            if (!_characterController.isGrounded)
            {
                _Velocity -= _characterController.transform.up * _gravity * Time.deltaTime;
                _characterController.Move(_Velocity);
            }
            if (_characterController.isGrounded)
            {
                _Velocity = Vector3.zero;
            }
        }
        public void Fall()
        {
            StartCoroutine(MoveDown());
        }
        public IEnumerator MoveDown()
        {
            _Velocity = Vector3.zero;
            while (!_characterController.isGrounded)
            {
                _Velocity = -_characterController.transform.up * _gravity * Time.deltaTime;
                _characterController.Move(_Velocity);
                yield return null;
            }
        }
    }
}
