using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    Transform[] gameObjects = new Transform[3];
    [SerializeField]
    Animator[] animator;
    readonly int Start_Hash = Animator.StringToHash("Start");
    readonly int Off_Hash = Animator.StringToHash("Off");

    public static int level;

    private void Awake()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        gameObjects[0] = canvas.transform.GetChild(0).GetChild(0).GetChild(0).transform;
        gameObjects[1] = canvas.transform.GetChild(0).GetChild(0).GetChild(1).transform;
        gameObjects[2] = canvas.transform.GetChild(0).GetChild(0).GetChild(2).transform;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObjects[i].gameObject.SetActive(false);
        }
    }

    public void SceneChangeNormal()
    {
        level = 1;
        StartCoroutine(SceneCoroutine());
    }

    public void SceneChaangeHard()
    {
        level = 2;
        StartCoroutine(SceneCoroutine());
    }

    public void SceneChangeHell()
    {
        level = 3;
        StartCoroutine(SceneCoroutine());
    }

    public void StartButton()
    {
        for(int i = 0; i < 3; i++)
        {
            gameObjects[i].gameObject.SetActive(true);
        }
        animator[0].SetTrigger(Start_Hash);
        animator[1].SetTrigger(Start_Hash);
    }

    IEnumerator SceneCoroutine()
    {
        animator[2].SetTrigger(Off_Hash);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene");
    }


}
