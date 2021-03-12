using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string firstLevel;
    [SerializeField]
    private string secondLevel;

    public void StartGame() => SceneManager.LoadScene(firstLevel);

    public void StartSecondLevel() => SceneManager.LoadScene(secondLevel);

    public void QuitGame() => Application.Quit();
}
