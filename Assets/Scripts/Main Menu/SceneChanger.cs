using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    const string PlayScene = "GameScene";

    private AsyncOperation _loadingOperation;

    public static Action OnSceneLoaded;  

    public void LoadScene(string sceneName)
    {
        _loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadingOperation.allowSceneActivation = false;
        StartCoroutine(LoadingScene());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private IEnumerator LoadingScene()
    {
        while (_loadingOperation.isDone == false)
        {
            if (_loadingOperation.progress >= 0.9f)
                _loadingOperation.allowSceneActivation = true;

            yield return null;
        }
    }
}
