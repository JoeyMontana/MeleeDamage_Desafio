using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameObject : MonoBehaviour {
    public float timer = 2f;

    private void Start() {
        Invoke("DeactivateAfterTime", timer);
    }

    private void DeactivateAfterTime() {
        Destroy(gameObject);
    }
}