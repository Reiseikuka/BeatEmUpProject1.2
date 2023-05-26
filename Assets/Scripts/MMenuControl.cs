using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace SA
{
    public class MMenuControl : MonoBehaviour
    {
        [Header("Volume Settings")]
        [SerializeField] private TMP_Text volumeTextValue = null;
        [SerializeField] private Slider volumeSlider = null;
        [SerializeField] private float defaultVolume = 1.0f;


        [Header("Gameplay Settings")]
        [SerializeField] private TMP_Text controllerSenTextValue = null;
        [SerializeField] private Slider controllerSenSlider = null;       
        [SerializeField] private int defaultSen = 4;
        public int mainControllerSen = 4;


        [Header("Toggle Settings")]
        [SerializeField] private Toggle invertYToggle = null;


        [Header("Confirmation")]
        [SerializeField] private GameObject confirmationPrompt = null;


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

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            volumeTextValue.text = volume.ToString("0.0");
        }

        public void VolumeApply()
        {
            PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
            StartCoroutine(ConfirmationBox());
        }

        public void SetControllerSen(float sensitivity)
        {
            mainControllerSen = Mathf.RoundToInt(sensitivity);
            controllerSenTextValue.text = sensitivity.ToString("0");
        }

        public void GameplayApply()
        {
            if (invertYToggle.isOn)
            {
                PlayerPrefs.SetInt("masterInvertY", 1);
                //Invert Y
            }
            else
            {
                PlayerPrefs.SetInt("masterInvertY", 0);
                //Not InvertY
            }

            PlayerPrefs.SetFloat("masterSen", mainControllerSen);
            StartCoroutine(ConfirmationBox());
        }

        public void ResetButton(string MenuType)
        {
            if (MenuType == "Audio")
            {
                AudioListener.volume = defaultVolume;
                volumeSlider.value = defaultVolume;
                volumeTextValue.text = defaultVolume.ToString("0.0");
                VolumeApply();
            }

            if (MenuType == "Gameplay")
            {
                controllerSenTextValue.text = defaultSen.ToString("0");
                controllerSenSlider.value = defaultSen;
                mainControllerSen = defaultSen;
                invertYToggle.isOn = false;
                GameplayApply();
            }
        }

        public IEnumerator ConfirmationBox()
        {
            confirmationPrompt.SetActive(true);
            yield return new WaitForSeconds(2);
            confirmationPrompt.SetActive(false);
        }
        
    }
}
