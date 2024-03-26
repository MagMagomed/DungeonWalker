using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private JumpFX _jumpFX;
        [SerializeField] private RigidBodyDecorator _rigidBodyDecorator;
        [SerializeField] private bool _canJump;
        private Debugger _debugger;
        private Transform _cameraTransform;
        private void Start()
        {
            // Get the camera transform
            _cameraTransform = Camera.main.transform;
            _jumpFX.AddOnJump(() => { _rigidBodyDecorator.SetUseGravity(false); });
            _jumpFX.AddOnJumpEnded(() => { _rigidBodyDecorator.SetUseGravity(true); });
        }
        private void Update()
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            float shift = Input.GetAxisRaw("Fire3");
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (shift > 0) Run(moveHorizontal, moveVertical); else Walk(moveHorizontal, moveVertical);
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
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;
            transform.rotation = new Quaternion(0, _cameraTransform.transform.rotation.y, 0, _cameraTransform.transform.rotation.w);
            transform.Translate(movement * speed * Time.deltaTime);
        }
        private void OnCollisionStay(Collision collision)
        {
            _canJump = true;
        }
        private void OnCollisionExit(Collision collision) 
        {
            _canJump = false;
        }
        private void Jump()
        {
            if(_canJump)
            {
                _jumpFX.PlayAnimation(transform);
            }
        }
        private void OnDestroy()
        {
            _jumpFX.RemoveOnJump(() => { _rigidBodyDecorator.SetUseGravity(false); });
            _jumpFX.RemoveOnJumpEnded(() => { _rigidBodyDecorator.SetUseGravity(true); });
        }
    }
}

