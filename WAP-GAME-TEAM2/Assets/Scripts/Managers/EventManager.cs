using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchType
{
    StartEvent,
    
}

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public bool[] switches;
    public bool isEventIng = false;
    

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    

    private void Start()
    {
        
    }
}
