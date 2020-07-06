﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    bool isCursorLocked = true;

    void LateUpdate()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isCursorLocked = !isCursorLocked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isCursorLocked = !isCursorLocked;
            }
        }

    }
}
