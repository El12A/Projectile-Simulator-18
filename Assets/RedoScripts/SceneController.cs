using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsProjectileSimulator
{
    // this class is used to manage all the scenes
    // It has three children classes each representing the different scenes
    // Scene 1 (index 0 in SceneManger) is the MainMenu, Scene 2 (index 1) is the PhysicsSimulator, Scene 3 (index 2) is the ProjectileEditor
    // it is used to save the important variables of some of the scripts so that their values aren't lost at scene change
    public class SceneController : GameController
    {
        private string saveFileName;
        private int SceneIndex;

        public void Save()
        {
            // Get the index of the current scene
            SceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Create a BinaryFormatter and open a file for writing
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + saveFileName);
            Debug.Log("file " + saveFileName + SceneIndex);
            // Serialize and write the index to the file
            bf.Serialize(file, SceneIndex);



            // Close the file
            file.Close();
        }
        public void Load()
        {
            Debug.Log("enter load");
            Debug.Log("file " + saveFileName + SceneIndex);
            // Check if the save file exists
            if (File.Exists(Application.persistentDataPath + "/" + saveFileName))
            {
                Debug.Log("enter load two");
                // Create a BinaryFormatter and open the file for reading
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + saveFileName, FileMode.Open);

                // Deserialize the index from the file
                SceneIndex = (int)bf.Deserialize(file);

                // Close the file
                file.Close();
            }
            // Load the last played scene
            SceneManager.LoadScene(SceneIndex);
        }

        // scene manager has scenes in a list. This project has my three scenes: main menu, physics simulator, projectile editor.
        // on call of this function the scene with the specific index referenced by the inputted indexOfScene integer, will be loaded.
        public void ChangeScene(int indexOfPreviousScene, int indexOfNewScene)
        {
            SetCorrectFileNameAndIndex(indexOfPreviousScene);
            Save();
            SetCorrectFileNameAndIndex(indexOfNewScene);
            Load();
        }
        // this will based on the index make the variable for saveFileName be correspondent name wise to the scene being saved or loaded.
        private void SetCorrectFileNameAndIndex(int index)
        {
            if (index == 0)
            {
                saveFileName = "MainMenuScene.dat";
            }
            if (index == 1)
            {
                saveFileName = "PhysicsSimulatorScene.dat";
            }
            if (index == 2)
            {
                saveFileName = "ProjectileEditor.dat";
            }
            SceneIndex = index;
        }
    }
}
