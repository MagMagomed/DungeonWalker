using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Transform _cameraTransform;
    private void Start()
    {
        // Get the camera transform
        _cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        float shift = Input.GetAxisRaw("Fire3");
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
}
