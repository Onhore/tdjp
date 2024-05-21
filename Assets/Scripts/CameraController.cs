using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GridBrushBase;

public class CameraController : MonoBehaviour
{
    public GameObject Camera;
    public float panSpeed = 30f;

    public float scrollSpeed = 5f;
    public Vector2 ScrollRange;
    public float rotationSpeed = 50f;
    public float smoothTimeMovement = 0.25f;
    public float smoothTimeScroll = 0.25f;
    public float smoothRotation = 0.25f;
    public Vector2 RotateRange;

    private Vector3 velocity = Vector3.zero;
    private Vector3 velocityZoom = Vector3.zero;
    private Vector3 rotateVelocity = Vector3.zero;
    private float rotateInput;
    private float scrollInput;
    private Vector3 moveInputX;
    private Vector3 moveInputY;
    private Vector3 scrollDirection;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;

    void Update()
    {
        CameraMove();

        CameraRotate();

        CameraScroll();
        
    }
    private void CameraMove()
    {
        moveInputX = Vector3.Scale(Camera.transform.forward.normalized, Vector3.forward + Vector3.right) * Input.GetAxisRaw("Vertical");
        moveInputY = Vector3.Scale(Camera.transform.right.normalized, Vector3.forward + Vector3.right) * Input.GetAxisRaw("Horizontal");
        moveDirection = transform.position + (moveInputX + moveInputY).normalized * panSpeed;
        //Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
        transform.position = Vector3.SmoothDamp(transform.position, moveDirection, ref velocity, smoothTimeMovement);
    }
    private void CameraRotate()
    {
        rotateInput = Input.GetAxisRaw("Rotation");
        rotationDirection = new Vector3(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y + rotationSpeed * rotateInput, RotateRange.x, RotateRange.y), transform.eulerAngles.z);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, rotationDirection, ref rotateVelocity, smoothRotation);
    }
    private void CameraScroll()
    {
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        scrollDirection = Camera.transform.position + Camera.transform.forward.normalized * scrollSpeed * scrollInput * 1000;
        
        Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, scrollDirection, ref velocityZoom, smoothTimeScroll);

        
    }
}