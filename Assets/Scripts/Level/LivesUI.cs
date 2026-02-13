using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Image[] hearts;

    public void UpdateLives(int lives)
    {
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("Hearts array no asignado en LivesUI");
            return;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
            {
                Debug.LogError("Heart " + i + " es NULL");
                continue;
            }

            hearts[i].enabled = i < lives;
        }
    }
}
