using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    Player player;

    Animator animator;
    AudioSource audioSource;

    float clipLength = 0.0f;
    PauseButton pauseButton;

    private void Awake()
    {
        GameObject main = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = main.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        AnimatorClipInfo info = animator.GetCurrentAnimatorClipInfo(0)[0];
        clipLength = info.clip.length;
        Canvas canvas = FindAnyObjectByType<Canvas>();
        pauseButton = canvas.transform.GetChild(7).GetComponent<PauseButton>();
        player = GameManager.Instance.Player;
    }

    private void OnEnable()
    {
        StartCoroutine(Panelfalse());
    }

    

    IEnumerator Panelfalse()
    {
        yield return new WaitForSeconds(clipLength);
    }

    public void TheEnd()
    {
        pauseButton.gameObject.SetActive(false);
        animator.SetTrigger("Clear");
        player.gameObject.SetActive(false);
    }

}
