using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneWithButton : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PrepareHost()
    {
        LoadingSceneManager.Instance.LoadMode = "Host";
    }

    public void PrepareSingle()
    {
        LoadingSceneManager.Instance.LoadMode = "Single";
    }

    public void HostLoadSceneFromManager(string sceneName)
    {
        LoadingSceneManager.Instance.LoadMode = "Host";
        LoadingSceneManager.Instance.SceneToLoad = sceneName;
        SceneManager.LoadScene(LoadingSceneManager.Instance.LoadingScreen);
    }

    public void ClientLoadSceneFromManager(string sceneName)
    {
        LoadingSceneManager.Instance.LoadMode = "Client";
        LoadingSceneManager.Instance.SceneToLoad = sceneName;
        SceneManager.LoadScene(LoadingSceneManager.Instance.LoadingScreen);
    }

    public void SingleLoadSceneFromManager(string sceneName)
    {
        LoadingSceneManager.Instance.LoadMode = "Single";
        LoadingSceneManager.Instance.SceneToLoad = sceneName;
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(LoadingSceneManager.Instance.LoadingScreen);
    }
}
