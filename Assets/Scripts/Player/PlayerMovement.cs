using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IRestartable
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private CharacterController characterController;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
        
        RestartController.instance.AddListener(this);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        characterController.Move(transform.forward * (moveSpeed * vertical * Time.deltaTime));
    }

    public void OnRestart()
    {
        characterController.enabled = false;
        transform.position = initialPos;
        characterController.enabled = true;
    }
}
