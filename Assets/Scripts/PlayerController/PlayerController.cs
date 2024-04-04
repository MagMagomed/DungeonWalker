using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _sprintSpeed = 10.0f;

        [SerializeField] private JumpFX _jumpFX;
        [SerializeField] private GravityFX _gravityFX;
        [SerializeField] private RigidBodyDecorator _rigidBodyDecorator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private InputController _input;

        [SerializeField] LayerMask _ground;
        [SerializeField] float _groundDistance;

        [SerializeField] Animator _animator;


        private Debugger _debugger;
        private Transform _cameraTransform;
        private bool _jumpInProgress;
        private void Start()
        {
            // Get the camera transform
            _cameraTransform = Camera.main.transform;
            _jumpFX.AddOnJump(() => { _jumpInProgress = true; });
            _jumpFX.AddOnJumpEnded(() => { _gravityFX.Fall(); _jumpInProgress = false; });
        }
        private void Update()
        {
            _animator.SetFloat("xVelocity", _input.MoveDirection.magnitude);
            if (_input.MoveDirection.magnitude > 0)
            {
                UpdateRotation();
                if (_input.Sprint) Run(_input.MoveDirection); else Walk(_input.MoveDirection);
            }
        }
        private void LateUpdate()
        {
            if (_input.Jump)
            {
                Jump(_input.MoveDirection);
            }
            if (!_jumpInProgress)
            {
                _gravityFX.Fall();
            }
        }
        private void Walk(Vector3 moveDirection)
        {
            Move(moveDirection, _speed);
        }
        private void Run(Vector3 moveDirection)
        {
            Move(moveDirection, _sprintSpeed);
        }
        private void Move(Vector3 moveDirection, float speed)
        {
            var moveVelocity = transform.rotation.normalized * moveDirection * speed * Time.deltaTime;
            if (IsGrounded())
            {
                _characterController.Move(moveVelocity);
            }
        }
        private void Jump(Vector3 moveDirection)
        {
            _jumpFX.PlayAnimation(transform, moveDirection);
        }
        private bool IsGrounded()
        {
            //return Physics.CheckSphere(transform.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
            return _characterController.detectCollisions;
        }
        private void UpdateRotation()
        {
            transform.rotation = new Quaternion(0, _cameraTransform.transform.rotation.y, 0, _cameraTransform.transform.rotation.w);
            _characterController.transform.rotation = transform.rotation;

        }
        private void OnDestroy()
        {
            _jumpFX.RemoveOnJump(() => { _jumpInProgress = true; });
            _jumpFX.RemoveOnJumpEnded(() => { _gravityFX.Fall(); _jumpInProgress = false; });
        }
    }
}

