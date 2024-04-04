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

namespace Assets.Scripts
{
    internal class GravityFX : MonoBehaviour
    {

        [SerializeField] CharacterController _characterController;
        [SerializeField] Transform _groundChecker;
        [SerializeField] LayerMask _ground;

        [SerializeField] float _gravity;
        [SerializeField] float _groundDistance;

        private bool _isGrounded;
        public void Fall()
        {
            StartCoroutine(MoveDown());
        }
        public IEnumerator MoveDown()
        {
            float yVelocity = 0;
            _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
            while (!_isGrounded)
            {
                _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
                yVelocity += _gravity * Time.deltaTime;
                _characterController.Move(new Vector3(0, -yVelocity, 0) * Time.deltaTime);
                yield return null;
            }
        }
    }
}
