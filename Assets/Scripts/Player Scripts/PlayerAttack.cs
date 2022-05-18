using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState {
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2,
    AIR_KICK
}

public class PlayerAttack : MonoBehaviour {
    private CharacterAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    
    private bool _activateTimerToReset;

    private const float DefaultComboTimer = 0.6f;
    private float _currentComboTimer;

    private ComboState _currentComboState;

    private void Awake() {
        _playerAnimation = GetComponentInChildren<CharacterAnimation>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        _currentComboTimer = DefaultComboTimer;
        _currentComboState = ComboState.NONE;
    }

    private void Update() {
        ComboAttacks();
        ResetComboState();
    }

    private void ComboAttacks() {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Joystick1Button2)) {
            if (_currentComboState is ComboState.PUNCH_3 or ComboState.KICK_1 or ComboState.KICK_2) {
                return;
            }

            _currentComboState++;
            _activateTimerToReset = true;
            _currentComboTimer = DefaultComboTimer;

            if (_currentComboState == ComboState.PUNCH_1) {
                _playerAnimation.Punch_1();
            }

            if (_currentComboState == ComboState.PUNCH_2) {
                _playerAnimation.Punch_2();
            }

            if (_currentComboState == ComboState.PUNCH_3) {
                _playerAnimation.Punch_3();
            }
        }

        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button3)) {
            if (_currentComboState is ComboState.KICK_2 or ComboState.PUNCH_3 or ComboState.AIR_KICK) {
                return;
            }

            if (_currentComboState is ComboState.NONE or ComboState.PUNCH_1 or ComboState.PUNCH_2 && _playerMovement.grounded) {
                _currentComboState = ComboState.KICK_1;
            }
            else if (_currentComboState == ComboState.KICK_1  && _playerMovement.grounded) {
                _currentComboState++;
            } else if (!_playerMovement.grounded) {
                _currentComboState = ComboState.AIR_KICK;
                _playerMovement.kicked = true;
            }

            _activateTimerToReset = true;
            _currentComboTimer = DefaultComboTimer;

            if (_currentComboState == ComboState.KICK_1) {
                _playerAnimation.Kick_1();
            }

            if (_currentComboState == ComboState.KICK_2) {
                _playerAnimation.Kick_2();
            }

            if (_currentComboState == ComboState.AIR_KICK) {
                _playerAnimation.AirKick();
            }
        }
    }

    private void ResetComboState() {
        if (_activateTimerToReset) {
            _currentComboTimer -= Time.deltaTime;

            if (_currentComboTimer < -0f) {
                _currentComboState = ComboState.NONE;

                _activateTimerToReset = false;
                _currentComboTimer = DefaultComboTimer;
            }
        }
    }
}