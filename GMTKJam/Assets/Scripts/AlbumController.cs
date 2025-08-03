using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer albumOne;
    public bool hasAlbumOne;

    [SerializeField]
    private SpriteRenderer albumTwo;
    public bool hasAlbumTwo;

    [SerializeField]
    private SpriteRenderer albumThree;
    public bool hasAlbumThree;

    [SerializeField]
    private SpriteRenderer albumFour;
    public bool hasAlbumFour;

    [SerializeField]
    private Image fadeOut;

    public static AlbumController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckCollected()
    {
        if (hasAlbumOne)
        {
            albumOne.DOColor(Color.white, 1.5f);
        }
        if (hasAlbumTwo)
        {
            albumTwo.DOColor(Color.white, 1.5f);
        }
        if (hasAlbumThree)
        {
            albumThree.DOColor(Color.white, 1.5f);
        }
        if (hasAlbumFour)
        {
            albumFour.DOColor(Color.white, 1.5f);
        }

        if (hasAlbumOne && hasAlbumTwo && hasAlbumThree && hasAlbumFour)
        {
            //Win game
            CameraShakeManager.Instance.Shake(3f);
            GameManager.Instance.PlayerPosition.DOMove(transform.position, 3f);
            GameManager.Instance.PlayerPosition.DOShakeRotation(3f);
            GameManager.Instance.PlayerPosition.DOScale(0, 3f);
            Invoke(nameof(FadeOut), 3f);
        }
    }

    private void FadeOut()
    {
        fadeOut
            .DOFade(255, 1f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(0);
            });
    }
}
