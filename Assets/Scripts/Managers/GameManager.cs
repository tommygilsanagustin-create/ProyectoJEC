using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives")]
    public int maxLives = 3;
    public int currentLives;
    public LivesUI livesUI;


    [Header("Checkpoint")]
    private Vector3 checkpointPosition;

    public bool hasKey = false;
    public int collectibles = 0;
    public int maxCollectibles = 4;

    public void AddCollectible()
    {
        collectibles++;
    }

    public void PickKey()
    {
        hasKey = true;
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;

        if (livesUI == null)
        {
            Debug.LogError("LivesUI NO asignado en GameManager");
            return;
        }

        livesUI.UpdateLives(currentLives);
    }



    public void SetCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
    }

    public void PlayerDied(GameObject player)
    {
        currentLives--;
        livesUI.UpdateLives(currentLives);

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer(player);
        }
    }


    void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        player.GetComponent<PlayerController>().Respawn();
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver"); // escena aparte
    }
}
