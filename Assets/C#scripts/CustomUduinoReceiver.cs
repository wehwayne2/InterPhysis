using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using Uduino;

public class CustomUduinoReceiver : MonoBehaviour
{
    public GameObject targetGameObject;
    public string customEventName;

    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        CustomEvent.Trigger(targetGameObject, customEventName, data);
    }
    
}