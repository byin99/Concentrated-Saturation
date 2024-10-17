using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverButton : MonoBehaviour
{
    Player player;
    public void SceneChangeStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void SceneChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
