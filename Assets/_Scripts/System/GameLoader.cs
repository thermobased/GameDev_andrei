using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class GameLoader
{
    private static GameLoader _instance;
    private static Canvas _loadingCanvas;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutostartGame()
    {
        _instance = new GameLoader();
        _ = RunGame();
    }

    public static async Task RunGame()
    {
        await LoadSceneAsync(Scenes.MAIN_MENU);
    }

    public static void LoadMainMenu()
    {
        _ = LoadSceneAsync(Scenes.MAIN_MENU);
    }
    public static async Task LoadSceneAsync(string targetScene)
    {
        Time.timeScale = 1f;
        ShowLoading();
        
        
        var bootOperation = SceneManager.LoadSceneAsync(Scenes.BOOT);
        while (!bootOperation.isDone)
            await Task.Yield();
        await Task.Delay(500);

        var targetOperation = SceneManager.LoadSceneAsync(targetScene);
        while (!targetOperation.isDone)
            await Task.Yield();

        HideLoading();
    }
    public static void LoadGameplay()
    {
        _ = LoadSceneAsync(Scenes.GAMEPLAY);
    }



    public GameLoader()
    {
        CreateLoadingCanvas();
    }

    private void CreateLoadingCanvas()
    {
        var loadingPrefab = Resources.Load<Canvas>("LoadingScreenPrefab");
        if (loadingPrefab != null)
        {
            _loadingCanvas = Object.Instantiate(loadingPrefab);
            Object.DontDestroyOnLoad(_loadingCanvas.gameObject);
            _loadingCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Can't find LoadingScreenPrefab");
        }
    }

    private static void ShowLoading()
    {
        if (_loadingCanvas != null)
        {
            _loadingCanvas.gameObject.SetActive(true);
        }
    }

    private static void HideLoading()
    {
        if (_loadingCanvas != null)
        {
            _loadingCanvas.gameObject.SetActive(false);
        }
    }
}
