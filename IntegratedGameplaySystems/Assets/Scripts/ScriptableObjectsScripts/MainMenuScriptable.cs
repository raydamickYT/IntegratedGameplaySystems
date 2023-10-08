using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MainMenuFunctions", menuName = "ScriptableObjects/MainMenu")]
public class MainMenuScriptable : ScriptableObject
{
    [SerializeField] private int sceneIndex;

    public void GoToTheNextScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
