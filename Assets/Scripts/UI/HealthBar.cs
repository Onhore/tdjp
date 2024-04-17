using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Progressive _health;
    [SerializeField] private Image _fillImage;

    private void OnEnable() => _health.OnChange += UpdateBAR;
    private void OnDisable() => _health.OnChange -= UpdateBAR;

    private void UpdateBAR() {_fillImage.fillAmount = _health.Ratio; Debug.Log(_health.Ratio);}
}
