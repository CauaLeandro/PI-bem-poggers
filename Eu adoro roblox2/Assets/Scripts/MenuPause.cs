using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SairPause()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("cu");
    }
    public void SairProMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        Debug.Log("cuzao");
    }
    public void RestartGay()
    {
        SceneManager.LoadScene("Menu");
    }
}
