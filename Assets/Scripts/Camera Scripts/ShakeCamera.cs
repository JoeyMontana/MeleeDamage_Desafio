using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeCamera : MonoBehaviour {
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slowDownAmount = 1f;

    private float _initialDuration;
    private bool _shouldShake;

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.localPosition;
        _initialDuration = duration;
    }

    private void Update() {
        Shake();
    }

    private void Shake() {
        if (_shouldShake) {
            if (duration > 0f) {
                transform.localPosition = _startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else {
                _shouldShake = false;
                duration = _initialDuration;
                transform.localPosition = _startPosition;
            }
        }
    }

    public bool ShouldShake {
        get => _shouldShake;
        set => _shouldShake = value;
    }
}