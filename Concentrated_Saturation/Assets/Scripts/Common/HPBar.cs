using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOver;
    Player player;
    Slider slider;
    public float currentHP;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        slider = transform.GetComponent<Slider>();
        currentHP = 1;
        slider.value = 1;
    }

    private void Update()
    {
        StartCoroutine(SetSliderValue());
    }

    IEnumerator SetSliderValue()
    {
        while(currentHP > player.PlayerHP)
        {
            currentHP -= Time.deltaTime * 0.1f;
            slider.value = currentHP;
            yield return null;
        }
    }
}
