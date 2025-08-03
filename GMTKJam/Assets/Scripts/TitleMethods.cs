using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMethods : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playButton;

    [SerializeField]
    private TextMeshProUGUI pauseButton;

    public void StartGame()
    {
        //Load scene
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayHover()
    {
        playButton.transform.DOScaleY(1, 0.3f);
    }

    public void PlayExit()
    {
        playButton.transform.DOScaleY(0, 0.3f);
    }

    public void QuitHover()
    {
        pauseButton.transform.DOScaleY(1, 0.3f);
    }

    public void QuitExit()
    {
        pauseButton.transform.DOScaleY(0, 0.3f);
    }
}
