using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSceneFunction(string sceneName)//Changes to Scene
    {
        SceneManager.LoadScene(sceneName);//Loads next Scene

    }

    public void ExitGame() //Funtion to exit game application
    {
        Application.Quit();
    }
}
