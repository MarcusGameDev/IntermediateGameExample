using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//A cheeky little comment
public class LoadScene : MonoBehaviour
{
    //This is being used just for the "OnTriggerEnter".
    public string sceneToLoad;

    public void SceneChange(string sceneName)
    {
        //Check if there is a "sceneToLoad". If there is, load that scene
        if(sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
        //If there is no other scene to load, load the next scene instead.
        else
        {
            // Gets the current scene, then loads the next scene in the build settings.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ReloadScene()
    {
        //Reload the current Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SpecificLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Reload Scene
            ReloadScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //      SceneChange(sceneToLoad);
            //   ReloadScene();
            // NextLevel();
            SpecificLevel(sceneToLoad);
        }
    }


}
