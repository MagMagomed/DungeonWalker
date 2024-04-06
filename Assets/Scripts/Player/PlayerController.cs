using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _sprintSpeed = 10.0f;
        [SerializeField] private float _rotationVelocity = 1f;
        [SerializeField] private float _rotationTime = 1f;

        [SerializeField] private JumpFX _jumpFX;
        [SerializeField] private GravityFX _gravityFX;
        [SerializeField] private RigidBodyDecorator _rigidBodyDecorator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private InputController _input;

        [SerializeField] LayerMask _ground;
        [SerializeField] float _groundDistance;

        [SerializeField] PlayerAnimationController _animationController;


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
            _animationController.SetXVelocity(_input.Sprint? _input.MoveDirection.magnitude * _sprintSpeed : _input.MoveDirection.magnitude * _speed);
            if (_input.MoveDirection.magnitude > 0)
            {
                UpdateRotation();
                if (_input.Sprint) Run(); else Walk();
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
        private void Walk()
        {
            Move(_speed);
        }
        private void Run()
        {
            Move(_sprintSpeed);
        }
        private void Move(float speed)
        {
            var moveVelocity = transform.forward * speed * Time.deltaTime;
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
            var targetRotation = Mathf.Atan2(_input.MoveDirection.x, _input.MoveDirection.z) * Mathf.Rad2Deg +
                                  _cameraTransform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, _rotationTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            _characterController.transform.rotation = transform.rotation;
        }
        private void OnDestroy()
        {
            _jumpFX.RemoveOnJump(() => { _jumpInProgress = true; });
            _jumpFX.RemoveOnJumpEnded(() => { _gravityFX.Fall(); _jumpInProgress = false; });
        }
    }
}

