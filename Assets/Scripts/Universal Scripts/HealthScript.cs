using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthScript : MonoBehaviour {
    public float health = 100f;

    private CharacterAnimation _animationScript;
    private EnemyMovement _enemyMovement;

    private bool _characterDied;

    public bool isPlayer;

    private HealthUI _healthUI;

    private void Awake() {
        _animationScript = GetComponentInChildren<CharacterAnimation>();
        if (isPlayer) {
            _healthUI = GetComponent<HealthUI>();
        }
    }

    public void ApplyDamage(float damage, bool knockDown) {
        if (_characterDied) {
            return;
        }

        health -= damage;

        if (isPlayer) {
            _healthUI.DisplayHealth(health);
        }

        if (health <= 0f) {
            _animationScript.Death();
            _characterDied = true;

            if (isPlayer) {
                GameObject.FindWithTag(Tags.ENEMY_TAG).GetComponent<EnemyMovement>().enabled = false;
            }

            return;
        }

        if (!isPlayer) {
            if (knockDown) {
                if (Random.Range(0, 2) > 0) {
                    _animationScript.KockDown();
                }
            }
            else {
                if (Random.Range(0, 3) > 1) {
                    _animationScript.Hit();
                }
            }
        }
    }
}