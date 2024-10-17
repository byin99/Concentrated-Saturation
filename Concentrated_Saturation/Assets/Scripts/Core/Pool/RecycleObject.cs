using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    public Action onDisable = null;

    protected virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero;         
        transform.localRotation = Quaternion.identity;

        StopAllCoroutines();    
    }

    protected virtual void OnDisable()
    {
        onDisable?.Invoke();        
    }
}
