using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private bool isInitialize = false;

    private static bool isShutdown = false;

    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (isShutdown)  
            {
                Debug.LogWarning("ΩÃ±€≈Ê¿Ã ªË¡¶ ¡ﬂø° ø‰±∏πﬁ¿Ω.");     
                return null;                                       
            }

            if (instance == null)
            {
                T singleton = FindAnyObjectByType<T>();
                if (singleton == null)
                {
                    // æ¿ø° ΩÃ±€≈Ê¿Ã æ¯¿Ω
                    GameObject obj = new GameObject();      
                    obj.name = $"{typeof(T)}_Singleton";    
                    singleton = obj.AddComponent<T>();      
                }
                instance = singleton;     
                DontDestroyOnLoad(instance.gameObject);     
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;       
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if (instance != this)           
            {
                Destroy(this.gameObject);   
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;      
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;     
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isInitialize)
        {
            OnPreInitialize();      
        }
        if (mode != LoadSceneMode.Additive)
        {
            OnInitialize();         
        }
    }

    protected virtual void OnPreInitialize()
    {
        isInitialize = true;
    }

    protected virtual void OnInitialize()
    {
    }

    private void OnApplicationQuit()
    {
        isShutdown = true;
    }
}
