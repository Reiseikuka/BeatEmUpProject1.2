using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SA
{
    public class MMenuControl : MonoBehaviour
    {
        [Header("Levels To Load")]
        public string _newGameLevel;
        //Load or Run at any point when we create a New Game
        private string levelToLoad;
        //Will Load our level when we need it
        [SerializeField] private GameObject noSavedGameDialog = null;


        public void NewGameDialogYes()
        {
            SceneManager.LoadScene(_newGameLevel);
            /*When we click Yes button in the options, we are going to load a scene
              that we ask*/
        }

        public void LoadGameDialogYes()
        {
            if(PlayerPrefs.HasKey("SavedLevel"))
            {
                levelToLoad = PlayerPrefs.GetString("SavedLevel");
                SceneManager.LoadScene(levelToLoad);
            } else
            {
                noSavedGameDialog.SetActive(true);
            }
            /*Anytime we want to load up a file, we check if at any moment, we have a file
              named "SavedLevel" If we don't have anything to load, load up the pop up
              dialog box that tells the Player there isn't a Load file*/
        }

        public void ExitButton()
        {
            Application.Quit();
        }
        
    }
}
