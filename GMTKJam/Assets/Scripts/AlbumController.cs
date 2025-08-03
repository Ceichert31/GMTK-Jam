using DG.Tweening;
using UnityEngine;

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

    public static AlbumController Instance;

    public void CheckCollected()
    {
        if (hasAlbumOne)
        {
            albumOne.DOColor(new Color(1, 1, 1, 1), 1.5f);
        }
        if (hasAlbumTwo)
        {
            albumTwo.DOColor(new Color(1, 1, 1, 1), 1.5f);
        }
        if (hasAlbumThree)
        {
            albumThree.DOColor(new Color(1, 1, 1, 1), 1.5f);
        }
        if (hasAlbumFour)
        {
            albumFour.DOColor(new Color(1, 1, 1, 1), 1.5f);
        }
    }
}
