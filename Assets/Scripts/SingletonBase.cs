using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static  T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject _singleton = new GameObject("_" + typeof(T).Name);
                    instance = _singleton.AddComponent<T>();
                }
            }	
            return instance;
        }
    }

    public virtual void Awake()
    {
        //Debug.Log("--public virtual void Awake()--");
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
