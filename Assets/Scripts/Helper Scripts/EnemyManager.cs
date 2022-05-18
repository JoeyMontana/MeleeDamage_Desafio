using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager Instance;

    public bool secondStage = false;
    [SerializeField] private GameObject enemyPrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void SpawnEnemy(int level) {
        if (level == 1) {
            for (int i = 0; i <= 1; i++) {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        } else if (level == 2) {
            StartCoroutine(StartStage2());
        }
    }

    IEnumerator StartStage2() {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i <= 3; i++) {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        secondStage = true;
    }
}