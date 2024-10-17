using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Sprite[] sprites;

    Image image;

    Image parentImage;

    bool isStop = false;

    Button button;

    TextMeshProUGUI monsters;

    EnemySpawner enemySpawner;

    GameObject main;
    AudioSource audioSource;

    private void Awake()
    {
        main = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = main.GetComponent<AudioSource>();
        parentImage = GetComponent<Image>();
        image = transform.GetChild(0).GetComponent<Image>();
        button = transform.GetChild(0).GetComponent<Button>();
        image.sprite = sprites[0];
        parentImage.enabled = false;
        monsters = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();

    }

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
    }

    private void Update()
    {
        monsters.text = $"목표 : {enemySpawner.killOfMonsters} / {enemySpawner.numberOfMonsters}";
    }

    void ClickButton()
    {
        if (!isStop)
        {
            PauseClick();
        }
        else
        {
            StartClick();
        }
    }

    void PauseClick()
    {
        image.sprite = sprites[1];
        Time.timeScale = 0;
        audioSource.mute = true;
        parentImage.enabled = true;
        isStop = true;
    }

    void StartClick()
    {
        image.sprite = sprites[0];
        Time.timeScale = 1;
        audioSource.mute = false;
        parentImage.enabled = false;
        isStop = false;
    }
}
