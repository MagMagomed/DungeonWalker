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
        [SerializeField] AnimationCurve _yAnimation;
        [SerializeField] LayerMask _ground;

        [SerializeField] float _gravity;
        [SerializeField] float _groundDistance;

        [SerializeField] float _progress;
        [SerializeField] float _evaluted;

        private bool _isGrounded;

        public void Update()
        {
            _isGrounded = Physics.CheckSphere(transform.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
            if (!_isGrounded)
            {
                PlayAnimation();
            }
        }
        public void PlayAnimation()
        {
            StartCoroutine(Animation());
        }
        private IEnumerator Animation()
        {
            float exprideSeconds = 0;
            while (!_isGrounded)
            {
                exprideSeconds += Time.deltaTime;
                _progress = exprideSeconds;
                float yVelocity = _yAnimation.Evaluate(exprideSeconds) * _gravity;
                _evaluted = yVelocity;
                _characterController.Move(new Vector3(0, -yVelocity, 0) * Time.deltaTime);
                yield return null;
            }
        }
    }
}
