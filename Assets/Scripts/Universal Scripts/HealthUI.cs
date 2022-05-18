using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    private Image _healthUI;

    private void Awake() {
        _healthUI = GameObject.FindWithTag(Tags.HEALTH_UI).GetComponent<Image>();
    }

    public void DisplayHealth(float value) {
        value /= 100f;

        if (value < 0f) {
            value = 0f;
        }

        _healthUI.fillAmount = value;
    }
}