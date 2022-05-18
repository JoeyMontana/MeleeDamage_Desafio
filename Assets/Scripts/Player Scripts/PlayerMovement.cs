using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private CharacterAnimation _playerAnimation;
    private Rigidbody _playerRigidbody;

    public float walkSpeed = 2f;
    public float zSpeed = 1.5f;

    public Vector3 jump;
    public float jumpForce = 10.0f;

    private const float RotationY = -90f;

    public bool grounded = true;
    
    public bool kicked = false;

    private void Awake() {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimation = GetComponentInChildren<CharacterAnimation>();
        transform.rotation = Quaternion.Euler(0f, RotationY, 0f);
    }

    private void Update() {
        PlayerJump();
        RotatePlayer();
        AnimatePlayerWalk();
    }

    private void FixedUpdate() {
        DetectMovement();
    }

    private void DetectMovement() {
        _playerRigidbody.velocity = new Vector3(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walkSpeed),
            _playerRigidbody.velocity.y, Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-zSpeed));
    }

    private void RotatePlayer() {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0) {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(RotationY), 0f);
        }
        else if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0) {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(RotationY), 0f);
        }
    }

    private void AnimatePlayerWalk() {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 || Input.GetAxisRaw(Axis.VERTICAL_AXIS) != 0) {
            _playerAnimation.Walk(true);
        }
        else {
            _playerAnimation.Walk(false);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(Tags.GROUND_TAG)) {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag(Tags.GROUND_TAG)) {
            grounded = false;
        }
    }

    private void PlayerJump() {
        if (grounded) {
            if (Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Joystick1Button0)) {
                _playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
                grounded = false;

                _playerAnimation.Jump();
                if (!kicked) {
                    _playerAnimation.FallDown();
                }
            }
        }
    }
}