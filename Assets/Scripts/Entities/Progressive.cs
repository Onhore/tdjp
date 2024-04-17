using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Progressive : MonoBehaviour
{
    [SerializeField] private float _initial;
    public float Initial 
    { 
        get 
        {
            return _initial;
        }
        set
        {
            _initial = value;
        }
    }
    private float _current;
    public float Current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = value;
            OnChange?.Invoke();
        }
    }
    public float Ratio => _current / _initial;
    public Action OnChange;

    private void Awake() => _current = _initial;
}
