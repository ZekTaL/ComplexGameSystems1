using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2 : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    private GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
            }
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
