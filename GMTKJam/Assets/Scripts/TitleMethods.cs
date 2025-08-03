using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMethods : MonoBehaviour
{
    public void StartGame()
    {
        //Load scene
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
