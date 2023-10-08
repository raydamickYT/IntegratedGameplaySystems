using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MainMenuFunctions", menuName = "ScriptableObjects/MainMenu")]
public class MainMenuScriptable : ScriptableObject
{
    [SerializeField] private int gameSceneIndex;
    [SerializeField] private int mainMenuSceneIndex;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    public void GoToTheNextScene()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}