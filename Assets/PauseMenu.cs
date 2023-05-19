using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SA
{
    public class PauseMenu : MonoBehaviour
    {

        [SerializeField] GameObject pauseMenu;
        public static bool isGamePaused = false;
        

        void Update()
        {

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        
        void PauseGame()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }

        public void SettingsMenu()
        {

        }

        public void ReturnMainMenu()
        {
            //SceneManager.LoadScene("MainMenu")
            //Load the Main Menu Screen  
        }
        public void ExitGame()
        {
            Application.Quit();   
            Debug.Log("Game has been closed");         
        }
    }

}
