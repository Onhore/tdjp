using System.Collections;
using System.Collections.Generic;
using BuildSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PositionSetter : MonoBehaviour
{
    [SerializeField] private Tower tower;
    public void SetPosition()
    {
        transform.localPosition = tower.TopPosition+new Vector3(0,.5f, 0);;
    }
    void LateUpdate()
    {
        SetRotation();
    }
    public void SetRotation()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
