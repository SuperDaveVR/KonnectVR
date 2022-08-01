using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

[DisallowMultipleComponent]
public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance;

    [SerializeField] private string LoadingSceneName = "LoadScreen";
    [SerializeField] private string DefaultSceneName = "Lobby";

    //Scene Load Variables
    enum Mode { SINGLE, HOST, SERVER, CLIENT }

    [SerializeField] private Mode loadMode = Mode.HOST;

    public string LoadingScreen { 
        get {
            return LoadingSceneName;
        }
    }

    public string SceneToLoad { get; set; }
    public string LoadMode {
        get
        {
            return loadMode.ToString();
        }

        set
        {
            loadMode = (Mode)System.Enum.Parse(typeof(Mode), value.ToUpper());
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }


    }

    void LoadScene()
    {
        if (SceneToLoad == null)
        {
            SceneToLoad = DefaultSceneName;
        }
        if (loadMode != Mode.SINGLE)
            LoadNetworkScene();
        else
            LoadSingleScene();
    }

    private void LoadNetworkScene()
    {
        switch(loadMode)
        {
            case Mode.HOST:
                HandleHost();
                break;
            case Mode.CLIENT:
                HandleClient();
                break;
            default: 
                ReturnToHome();
                break;
        }

    }

    private async void HandleHost()
    {
        if (ConnectionManager.Instance.IsRelayEnabled) { 
            try
            {
                await ConnectionManager.Instance.SetupRelay();
            } catch (Exception ex)
            {
                ErrorManager.Instance.ThrowError(ex.Message, true);
            }
        }
        if (NetworkManager.Singleton.StartHost())
        {
            Debug.Log("Starting host!");
            if(NetworkManager.Singleton.IsListening)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
            } else
            {
                NetworkManager.Singleton.Shutdown();
                ReturnToHome();
            }
        }
        else
        {
            ErrorManager.Instance.ThrowError("Unable to start host...", true);
            ReturnToHome();
        }
    }

    private async void HandleClient()
    {
        if (ConnectionManager.Instance.IsRelayEnabled) { 
            try
            {
                await ConnectionManager.Instance.JoinRelay();
            }
            catch (Exception ex)
            {
                ErrorManager.Instance.ThrowError(ex.Message, true);
            }
        }
        if (NetworkManager.Singleton.StartClient()) 
        { 
            Debug.Log("Client started..."); 
        }
        else
        {
            ErrorManager.Instance.ThrowError("Unable to start client...", true);
            ReturnToHome();
        }
    }

    private void LoadSingleScene()
    {
        SceneManager.LoadSceneAsync(SceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log(scene.name);

        if (scene.name.Equals(LoadingSceneName))
        {
            LoadScene();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ReturnToHome()
    {
        SceneToLoad = DefaultSceneName;
        LoadSingleScene();
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
}
