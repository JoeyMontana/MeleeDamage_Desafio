using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour {
    public void RestartScene() {
        SceneManager.LoadScene(0);
        gameObject.SetActive(false);
    }
}