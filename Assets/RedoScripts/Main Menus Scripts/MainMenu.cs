using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PhysicsProjectileSimulator
{
    // this class handles all the gameobjects in the main menu scene
    public class MainMenu : SceneController
    {
        public GameObject MainMenuObject;
        public GameObject SettingsMenuObject;
        public GameObject InstructionsMenuObject;

        // reference in editor for the dropdowns and sliderS
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown frameRateDropdown;
        [SerializeField] private Slider volumeSlider;
        
        private GameObject gameMusic;
        // Start is called before the first frame update
        void Start()
        {
            // find music gameobject by seraching for gameobject with this tag
            // this is because gameMusic is persistant through scenes
            gameMusic = GameObject.FindWithTag("backgroundMusic");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPlayButtonClick()
        {
            // going from main menu scene index 0 to physics simulator scene index 1
            ChangeScene(0, 1);
        }
        public void OnQuitButtonClick()
        {
            Application.Quit();
        }
        // used for any of the buttons switching to and from main menu, setings menu and Instructions menu.
        public void SwitchMenu(GameObject menuOff, GameObject menuOn)
        {
            menuOff.SetActive(false);
            menuOn.SetActive(true);
        }

        // On resolution change update the screen resolution of the game
        // this is triggered my a new options being chosen from the resolution dropdown
        public void OnResolutionDropdownChange()
        {
            int index = resolutionDropdown.value;
            if (resolutionDropdown.options[index].text == "1920 x 1080 (FHD)")
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if (resolutionDropdown.options[index].text == "1366 x 768 (HD)")
            {
                Screen.SetResolution(1366, 768, true);
            }
            else if (resolutionDropdown.options[index].text == "2560x1440 (QHD)")
            {
                Screen.SetResolution(2560, 1440, true);
            }
            else if (resolutionDropdown.options[index].text == "3840 x 2160 (UHD)")
            {
                Screen.SetResolution(3840, 2160, true);
            }
        }
        //triggered on new option chosen from framerate dropdown
        public void OnFrameRateDropdownChange()
        {
            int index = frameRateDropdown.value;
            // caps the games framreate a specfic value e.g. 60hz = 60 screen refreshes every second
            if (frameRateDropdown.options[index].text == "60hz")
            {
                Application.targetFrameRate = 60;
            }
            else if (frameRateDropdown.options[index].text == "30hz")
            {
                Application.targetFrameRate = 30;
            }
            else if (frameRateDropdown.options[index].text == "120hz")
            {
                Application.targetFrameRate = 120;
            }
            else if (frameRateDropdown.options[index].text == "144hz")
            {
                Application.targetFrameRate = 144;
            }
            // no cap on the framerate runs as fast as possible
            else if (frameRateDropdown.options[index].text == "Unlimited")
            {
                Application.targetFrameRate = -1;
            }
        }
        // adjusts the volume of the audio component which contains looping background music of game
        public void OnVolumeSliderValueChange()
        {
            AudioSource backgroundMusic = gameMusic.GetComponent<AudioSource>();
            backgroundMusic.volume = volumeSlider.value;
        }
    }
}
