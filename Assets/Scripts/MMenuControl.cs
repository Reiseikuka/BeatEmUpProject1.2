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



        [Header("Graphics Settings")]
        [SerializeField] private Slider brightnessSlider = null;
        [SerializeField] private TMP_Text brightnessTextValue = null;
        [SerializeField] private float defaultBrightness = 1;

        private int _qualityLevel;
        private bool _isFullScreen;
        private float _brightnessLevel;

        [Space(10)]
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private Toggle fullScreenToggle;
        

        [Header("Confirmation")]
        [SerializeField] private GameObject confirmationPrompt = null;


        [Header("Levels To Load")]
        public string _newGameLevel;
        //Load or Run at any point when we create a New Game
        private string levelToLoad;
        //Will Load our level when we need it
        [SerializeField] private GameObject noSavedGameDialog = null;


      [Header("Resolutions  Dropdowns")]
      public  TMP_Dropdown resolutionDropdown;
      private Resolution[] resolutions;

      private void Start()
      {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        /*We need to get the index of any particular resolution
            because we will have to specify by name; So we need to  search
            through the length of our array to find how many
            different resolutions that we have;
            
            In other words:  We are going to get all
                the resolutions, we clear the options, create a list,
                of options that we'll currently have, we search through the
                length of the array,  we put the width and height into a string,
                and we will check if the resolutions that we found is 
                equal to our screen or height and then we'll set the 
                current resolution we want to chose.*/

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

      }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        /* Method that allow us to change the resolution*/

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

        public void SetBrightness(float brightness)
        {
            _brightnessLevel = brightness;
            brightnessTextValue.text = brightness.ToString("0.0");
        }

        public void SetFullScreen(bool isFullScreen)
        {
            _isFullScreen = isFullScreen;
        }

        public void SetQuality(int qualityIndex)
        {
            _qualityLevel = qualityIndex;
        }

        public void GraphicsApply()
        {
            PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
            //Change your brightness with postprocessing

            PlayerPrefs.SetInt("masterQuality", _qualityLevel);
            QualitySettings.SetQualityLevel(_qualityLevel);
            /*When we set our quality level  based on the settings;
            When applied, it will change to the Quality of the Index
            we just changed it */

            PlayerPrefs.SetInt("masterFullscreen",  (_isFullScreen ? 1 : 0));
            Screen.fullScreen = _isFullScreen;

            StartCoroutine(ConfirmationBox());
            /*When we apply the graphics, we are able to save what boolean it is,
              set it and then save the quality and save the brightness .*/
        }

        public void ResetButton(string MenuType)
        {

            if (MenuType == "Graphics")
            {
                //Reset Brightness v alue
                brightnessSlider.value = defaultBrightness;
                brightnessTextValue.text = defaultBrightness.ToString("0.0");

                qualityDropdown.value = 1;
                QualitySettings.SetQualityLevel(1);

                fullScreenToggle.isOn = false;
                Screen.fullScreen = false;

                Resolution currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
                resolutionDropdown.value = resolutions.Length;
                GraphicsApply();
            }

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
