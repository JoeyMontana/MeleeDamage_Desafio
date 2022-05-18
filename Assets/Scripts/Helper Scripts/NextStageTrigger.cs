using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageTrigger : MonoBehaviour {
    [SerializeField] private int id;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Tags.PLAYER_TAG)) {
            GameEvents.Current.NextStageTriggerEnter(id);
        }
    }
}