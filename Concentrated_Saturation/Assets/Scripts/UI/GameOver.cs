using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Animator animator;
    Player player;
    PauseButton pauseButton;
    readonly int GameOver_Hash = Animator.StringToHash("GameOver");

    private void Awake()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        pauseButton = canvas.transform.GetChild(7).GetComponent<PauseButton>();
        animator = GetComponent<Animator>();
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (player.PlayerHP < 0)
        {
            animator.SetTrigger(GameOver_Hash);
            pauseButton.gameObject.SetActive(false);
        }
    }
}
