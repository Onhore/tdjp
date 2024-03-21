using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    public bool bordersMovement = false;
    public float smoothTimeMovement = 0.25f;
    public float smoothTimeScroll = 0.25f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {

        /*if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }*/

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && bordersMovement)
        {
            //transform.Translate(Vector3.Scale(transform.forward.normalized,(Vector3.forward+Vector3.right)) * panSpeed * Time.deltaTime, Space.World);
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + Vector3.Scale(transform.forward.normalized, (Vector3.forward + Vector3.right)) * panSpeed, ref velocity, smoothTimeMovement);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness && bordersMovement)
        {
            //transform.Translate(-1*Vector3.Scale(transform.forward.normalized, (Vector3.forward + Vector3.right)) * panSpeed * Time.deltaTime, Space.World);
            transform.position = Vector3.SmoothDamp(transform.position, transform.position - Vector3.Scale(transform.forward.normalized, (Vector3.forward + Vector3.right)) * panSpeed, ref velocity, smoothTimeMovement);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness && bordersMovement)
        {
            //transform.Translate(Vector3.Scale(transform.right.normalized, (Vector3.forward + Vector3.right)) * panSpeed * Time.deltaTime, Space.World);
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + Vector3.Scale(transform.right.normalized, (Vector3.forward + Vector3.right)) * panSpeed, ref velocity, smoothTimeMovement);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness && bordersMovement)
        {
            //transform.Translate(-1 * Vector3.Scale(transform.right.normalized, (Vector3.forward + Vector3.right)) * panSpeed * Time.deltaTime, Space.World);
            transform.position = Vector3.SmoothDamp(transform.position, transform.position - Vector3.Scale(transform.right.normalized, (Vector3.forward + Vector3.right)) * panSpeed, ref velocity, smoothTimeMovement);
        }

        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //Vector3 pos = transform.position;
        
        //Debug.Log(transform.forward.normalized + " " + scroll);
        //pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);

        //transform.position = pos;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + transform.forward.normalized * scrollSpeed * scroll * 1000, ref velocity, smoothTimeScroll);

        //transform.position += transform.forward.normalized * scrollSpeed * scroll * 1000 * Time.deltaTime;
    }
}