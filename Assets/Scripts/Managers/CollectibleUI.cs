using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    public static CollectiblesUI Instance;

    public Image[] icons;

    void Awake()
    {
        Instance = this;
    }

    public void ActivateIcon(int id)
    {
        if (id < 0 || id >= icons.Length) return;

        icons[id].color = Color.white;
    }
}
