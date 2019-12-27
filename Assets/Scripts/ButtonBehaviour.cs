using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{

    public void OnButtonPressed(int sceneId) {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneId);
    }
}
