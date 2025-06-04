using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public Text Chanse;
    public Slider slider;
   
    public void NewZnachenie()
    {
        Chanse.text = (int) slider.value + "%";
        PlayerPrefs.SetInt("chance", (int) slider.value);
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menuu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        // Останавливает игру в редакторе
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Закрывает приложение в билде
        Application.Quit();
#endif
    }
}
