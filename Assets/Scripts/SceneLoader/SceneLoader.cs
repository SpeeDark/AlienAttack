using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject LoadingCanvas;

    private static SceneLoader Instance;

    private AsyncOperation loadingSceneOperation = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        LoadingCanvas.SetActive(false);
    }
    private void Update()
    {
        if (loadingSceneOperation == null)
            return;
        
        if (loadingSceneOperation.progress >= 1)
        {
            LoadingCanvas.SetActive(false);

            loadingSceneOperation = null;
        }
    }

    public static void SwitchScene(string SceneName)
    {
        Instance.LoadingCanvas.SetActive(true);

        Instance.loadingSceneOperation = SceneManager.LoadSceneAsync(SceneName);
        Instance.loadingSceneOperation.allowSceneActivation = true;
    }
}