using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EXBar : MonoBehaviour
{
    Player player;
    Slider slider;
    float currentEX;

    Animator animator;

    TextMeshProUGUI visualLevel;

    readonly int Level_Hash = Animator.StringToHash("LevelUp"); 

    int level;

    public int Level
    {
        get => level;
        private set
        {
            level = value;
        }
    }

    private void Awake()
    {
        player = GameManager.Instance.Player;
        slider = gameObject.GetComponent<Slider>();
        level = 1;
        Transform child = transform.GetChild(1);
        visualLevel = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        animator = child.GetComponent<Animator>();

    }

    private void Start()
    {
        currentEX = player.PlayerExperience;
        StartCoroutine(SetSliderValue());
    }

    IEnumerator SetSliderValue()
    {
        while(true)
        {
            while (currentEX < player.PlayerExperience)
            {
                currentEX += Time.deltaTime * 0.05f;
                slider.value = currentEX;
                yield return null;
            }
            if (currentEX > 0.99)
            {
                player.LevelUp();
                currentEX = player.PlayerExperience;
                slider.value = currentEX;
                StartCoroutine(LevelUpText());
            }
            visualLevel.text = $"Level : {player.PlayerLevel}";
            yield return null;
        }
    }

    IEnumerator LevelUpText()
    {
        animator.SetBool(Level_Hash, true);
        yield return new WaitForSeconds(2);
        animator.SetBool(Level_Hash, false);
    }
}
