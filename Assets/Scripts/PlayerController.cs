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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.rotation = new Quaternion(0, _cameraTransform.transform.rotation.y, 0, _cameraTransform.transform.rotation.w);
        transform.Translate(movement * _speed * Time.deltaTime);
    }
}
