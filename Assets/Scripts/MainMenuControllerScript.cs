using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControllerScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public void PlayGame()
    {
        StartCoroutine(LoadAsyncronously());
    }

    IEnumerator LoadAsyncronously()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            slider.value = progress;
            yield return null;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
