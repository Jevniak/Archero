using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static UISystem inst;

    [SerializeField] private List<GameObject> windows; 

    private void Start()
    {
        inst = this;
    }

    public void ChangeWindow(int id = -1)
    {
        /*
         * 0 - StartGame
         * 1 - Win
         */
        foreach (GameObject window in windows)
        {
            window.SetActive(false);
        }

        if (id != -1)
            windows[id].SetActive(true);
    }
}
