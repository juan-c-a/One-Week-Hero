using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartGameButton()
    {
        SceneManager.LoadScene("LoadoutSelection");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
