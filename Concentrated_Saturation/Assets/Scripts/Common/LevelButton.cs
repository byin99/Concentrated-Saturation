using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public static int level;

    public void SceneChangeNormal()
    {
        level = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void SceneChaangeHard()
    {
        level = 2;
        SceneManager.LoadScene("GameScene");
    }

    public void SceneChangeHell()
    {
        level = 3;
        SceneManager.LoadScene("GameScene");
    }
}
