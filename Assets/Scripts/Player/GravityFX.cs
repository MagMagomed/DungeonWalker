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

        [SerializeField] CharacterController _characterController;
        [SerializeField] Transform _groundChecker;
        [SerializeField] LayerMask _ground;
        [SerializeField] Vector3 _offsetSphereCast;

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
            var sphereCastCenter = transform.position + _offsetSphereCast;
            _isGrounded = Physics.CheckSphere(sphereCastCenter, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
            while (!_isGrounded)
            {
                _isGrounded = Physics.CheckSphere(sphereCastCenter, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
                yVelocity += _gravity * Time.deltaTime;
                _characterController.Move(new Vector3(0, -yVelocity, 0) * Time.deltaTime);
                yield return null;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + _offsetSphereCast, _groundDistance);
        }
    }
}
