using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour {
    private CharacterAnimation _enemyAnimation;

    private Rigidbody _enemyRigidbody;
    public float speed = 1.8f;

    private Transform _playerTarget;

    public float attackDistance = 2f;
    private float _currentAttackTime;
    private const float ChasePlayerAfterAttack = 0.2f;
    private const float DefaultAttackTime = 2f;

    private bool _followPlayer, _attackPlayer;

    private void Awake() {
        _enemyAnimation = GetComponentInChildren<CharacterAnimation>();
        _enemyRigidbody = GetComponent<Rigidbody>();

        _playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    private void Start() {
        _followPlayer = true;
        _currentAttackTime = DefaultAttackTime;
    }

    private void Update() {
        Attack();
    }

    private void FixedUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        if (!_followPlayer) {
            return;
        }

        if (Vector3.Distance(transform.position, _playerTarget.position) > attackDistance) {
            transform.LookAt(_playerTarget);
            _enemyRigidbody.velocity = transform.forward * speed;

            if (_enemyRigidbody.velocity.sqrMagnitude != 0) {
                _enemyAnimation.Walk(true);
            }
        }
        else if (Vector3.Distance(transform.position, _playerTarget.position) <= attackDistance) {
            _enemyRigidbody.velocity = Vector3.zero;
            _enemyAnimation.Walk(false);

            _followPlayer = false;
            _attackPlayer = true;
        }
    }

    private void Attack() {
        if (!_attackPlayer) {
            return;
        }

        _currentAttackTime += Time.deltaTime;

        if (_currentAttackTime > DefaultAttackTime) {
            _enemyAnimation.EnemyAttack(Random.Range(0, 3));
            _currentAttackTime = 0f;
        }

        if (Vector3.Distance(transform.position, _playerTarget.position) > attackDistance + ChasePlayerAfterAttack) {
            _attackPlayer = false;
            _followPlayer = true;
        }
    }
}