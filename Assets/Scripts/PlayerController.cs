using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private JumpFX _jumpFX;
        [SerializeField] private GravityFX _gravityFX;
        [SerializeField] private RigidBodyDecorator _rigidBodyDecorator;
        [SerializeField] CharacterController _characterController;
        private Debugger _debugger;
        private Transform _cameraTransform;

        private void Start()
        {
            // Get the camera transform
            _cameraTransform = Camera.main.transform;
            _jumpFX.AddOnJumpEnded(() => { _gravityFX.PlayAnimation(); });
        }
        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            float shift = Input.GetAxisRaw("Fire3");

            if (shift > 0) Run(moveHorizontal, moveVertical); else Walk(moveHorizontal, moveVertical);
        }
        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        private void Walk(float moveHorizontal, float moveVertical)
        {
            Move(moveHorizontal, moveVertical, _speed);
        }
        private void Run(float moveHorizontal, float moveVertical)
        {
            Move(moveHorizontal, moveVertical, _speed * 2);
        }
        private void Move(float moveHorizontal, float moveVertical, float speed)
        {
            if (object.ReferenceEquals(_characterController, null)) throw new System.InvalidOperationException("CharacterMotor must be initialized with an appropriate CharacterController.");
            var mv = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;

            if (_characterController.detectCollisions)
            {
                _characterController.Move(mv);
            }
        }
        private void Jump()
        {
            _jumpFX.PlayAnimation(transform, transform.forward);
        }
        private void OnDestroy()
        {
            _jumpFX.RemoveOnJumpEnded(() => { _gravityFX.PlayAnimation(); });
        }
    }
}

