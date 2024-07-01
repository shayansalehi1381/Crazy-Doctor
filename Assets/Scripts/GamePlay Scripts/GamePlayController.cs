using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField]
    private Text enemyKillCountText;

    private int enemyKillCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        enemyKillCountText.text = "Enemies Killed: " + enemyKillCount;
    }

    public void RestartGame()
    {
        Invoke("Restart", 3f);
    }

    void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
}
