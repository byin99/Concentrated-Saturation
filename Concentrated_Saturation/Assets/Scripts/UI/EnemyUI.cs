using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Slider bossSlider;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        bossSlider = transform.GetChild(1).GetComponent<Slider>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }


}
