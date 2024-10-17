using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stages;

    public AudioClip[] sounds;
    AudioSource audioSource;
    
    PauseButton pauseButton;

    static int stage;

    [SerializeField]
    Animator[] animator;

    readonly int Clear_Hash = Animator.StringToHash("Clear");
    readonly int Stage_Hash = Animator.StringToHash("Stage");
    readonly int StageClear_Hash = Animator.StringToHash("StageClear");

    private void Awake()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        pauseButton = canvas.transform.GetChild(7).GetComponent<PauseButton>();

        GameObject main = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = main.GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    public void ShowStage(int index)
    {
        StartCoroutine(MusicChange(index));
        animator[index].SetBool(Stage_Hash, true);
        if(index > 0)
        {
            animator[index - 1].SetBool(Stage_Hash, false);
        }
    }

    public void Clear()
    {
        animator[5].SetBool(Clear_Hash, true);
    }

    public void ClearOut()
    {
        animator[5].SetBool(Clear_Hash, false);
    }

    public void GameClear()
    {
        animator[6].SetTrigger(StageClear_Hash);
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2.0f);
        pauseButton.gameObject.SetActive(true);
    }

    IEnumerator MusicChange(int index)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime * 0.5f;
            audioSource.volume = 1 - timeElapsed;
            yield return null;
        }
        timeElapsed = 0.0f;
        audioSource.clip = sounds[index];
        audioSource.Play();
        while (timeElapsed < 1.0f)
        {
            timeElapsed += Time.deltaTime * 0.5f;
            audioSource.volume = timeElapsed;
            yield return null;
        }
    }
}
