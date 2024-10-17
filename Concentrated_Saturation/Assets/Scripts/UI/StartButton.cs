using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Enter");
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exit");
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
