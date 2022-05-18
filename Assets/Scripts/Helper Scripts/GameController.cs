using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject track, cmCamera, restartMenu;

    [SerializeField] private GameObject goToNextStage, startNextStage, enemyManager;

    private bool _startNewStage;
    private int _currentLevel;

    private void Start() {
        _currentLevel = 1;
        EnemyManager.Instance.SpawnEnemy(_currentLevel);
        GameEvents.Current.OnNextStageTriggerEnter += OnNextStage;
    }

    private void Update() {
        CheckEnemiesInLevel();
        InstantiateEnemies();
    }

    private void InstantiateEnemies() {
        if (_currentLevel >= 2) {
            return;
        }

        if (_startNewStage) {
            _currentLevel = 2;
            EnemyManager.Instance.SpawnEnemy(_currentLevel);
        }
    }

    private void CheckEnemiesInLevel() {
        if (_currentLevel == 1) {
            if (GameObject.FindWithTag(Tags.ENEMY_TAG) == null) {
                goToNextStage.SetActive(true);
            }
        }
        if (_currentLevel == 2 && enemyManager.GetComponent<EnemyManager>().secondStage) {
            if (GameObject.FindWithTag(Tags.ENEMY_TAG) == null) {
                StartCoroutine(Wait());
            }
        }
    }

    private void OnNextStage(int id) {
        if (id == 1) {
            startNextStage.SetActive(true);
            track.SetActive(true);
            cmCamera.SetActive(true);
            goToNextStage.SetActive(false);
        }
        else if (id == 2) {
            enemyManager.GetComponent<Transform>().position = new Vector3(-2f, 0.1f, -0.76f);
            cmCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
            cmCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
            _startNewStage = true;
            goToNextStage.SetActive(false);
        }
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(2f);
        restartMenu.SetActive(true);
    }
}