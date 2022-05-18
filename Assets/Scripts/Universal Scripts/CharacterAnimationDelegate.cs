using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour {
    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftLegAttackPoint, rightLegAttackPoint, enemy, restartMenu;

    public float standUpTimer = 2f;

    private CharacterAnimation _animationScript;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip whooshSound, fallSound, groundHitSound, deadSound;

    private EnemyMovement _enemyMovement;

    private ShakeCamera _shakeCamera;

    private void Awake() {
        _animationScript = GetComponent<CharacterAnimation>();
        _audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(Tags.ENEMY_TAG)) {
            _enemyMovement = GetComponentInParent<EnemyMovement>();
        }

        _shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();
    }

    private void LeftArmAttackOn() {
        leftArmAttackPoint.SetActive(true);
    }

    private void LeftArmAttackOff() {
        if (leftArmAttackPoint.activeInHierarchy) {
            leftArmAttackPoint.SetActive(false);
        }
    }

    private void RightArmAttackOn() {
        rightArmAttackPoint.SetActive(true);
    }

    private void RightArmAttackOff() {
        if (rightArmAttackPoint.activeInHierarchy) {
            rightArmAttackPoint.SetActive(false);
        }
    }

    private void LeftLegAttackOn() {
        leftLegAttackPoint.SetActive(true);
    }

    private void LeftLegAttackOff() {
        if (leftLegAttackPoint.activeInHierarchy) {
            leftLegAttackPoint.SetActive(false);
        }
    }

    private void RightLegAttackOn() {
        rightLegAttackPoint.SetActive(true);
    }

    private void RightLegAttackOff() {
        if (rightLegAttackPoint.activeInHierarchy) {
            rightLegAttackPoint.SetActive(false);
        }
    }

    private void TagLeftArm() {
        leftArmAttackPoint.tag = Tags.LEFT_ARM_TAG;
    }

    private void UntagLeftArm() {
        leftArmAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    private void TagLeftLeg() {
        leftLegAttackPoint.tag = Tags.LEFT_LEG_TAG;
    }

    private void UntagLeftLeg() {
        leftLegAttackPoint.tag = Tags.UNTAGGED_TAG;
    }

    private void EnemyStandUp() {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime() {
        yield return new WaitForSeconds(standUpTimer);
        _animationScript.StandUp();
    }

    public void AttackFXSound() {
        _audioSource.volume = 0.2f;
        _audioSource.clip = whooshSound;
        _audioSource.Play();
    }

    private void CharacterDieSound() {
        _audioSource.volume = 1f;
        _audioSource.clip = deadSound;
        _audioSource.Play();
    }

    private void EnemyKnockDown() {
        _audioSource.clip = fallSound;
        _audioSource.Play();
    }

    private void EnemyHitGround() {
        _audioSource.clip = groundHitSound;
        _audioSource.Play();
    }

    private void DisableMovement() {
        _enemyMovement.enabled = false;

        transform.parent.gameObject.layer = 0;
    }

    private void EnableMovement() {
        _enemyMovement.enabled = true;

        transform.parent.gameObject.layer = 7;
    }

    private void ShakeCameraOnFall() {
        _shakeCamera.ShouldShake = true;
    }

    private void CharacterDied() {
        Invoke("DeactivateGameObject", 2f);
    }

    private void DeactivateGameObject() {
        Destroy(enemy);
    }

    private void RestartMenu() {
        restartMenu.SetActive(true);
    }
}