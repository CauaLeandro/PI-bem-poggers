using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Player player;
    public GameObject restartCanva;
    public void Start()
    {
        player = GetComponent<Player>();
        restartCanva.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(player.Life <= 0)
        {
            restartCanva.SetActive(true);
        }
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
