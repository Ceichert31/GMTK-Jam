using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MP3UIController : MonoBehaviour
{
    //Update song duration, icon, name, and progress bar

    [SerializeField]
    private RectTransform fillBar;

    [SerializeField]
    private Image songIcon;

    [SerializeField]
    private TextMeshProUGUI songNameText;

    [SerializeField]
    private RectTransform mp3Player;

    [SerializeField]
    private RectTransform pauseButton;

    [SerializeField]
    private RectTransform forwardButton;

    private float songTimer;
    private float songLength;

    private void Update()
    {
        songTimer += Time.deltaTime;

        if (songTimer >= songLength)
        {
            ResetUI();
        }
    }

    private void ResetUI()
    {
        //Reset song timer
        songTimer = 0;
        fillBar.anchoredPosition = startPos;
    }

    public void UpdateSongInfo(SongEvent ctx)
    {
        ResetUI();
        songNameText.text = ctx.SongValue.songName;
        songLength = ctx.SongValue.songFile.length;
        songIcon.sprite = ctx.SongValue.songIcon;
        StartCoroutine(MoveSongBar());
    }

    Vector2 startPos = new Vector2(-276, 3.300005f);
    Vector2 endPos = new Vector2(0, 3.300005f);

    private IEnumerator MoveSongBar()
    {
        while (songTimer < songLength)
        {
            Vector2 intVector = Vector2.Lerp(startPos, endPos, songTimer / songLength);

            intVector.Set(Mathf.RoundToInt(intVector.x), Mathf.RoundToInt(intVector.y));

            fillBar.anchoredPosition = intVector;

            yield return null;
        }
        fillBar.anchoredPosition = endPos;
    }

    public void ExpandPlayer()
    {
        mp3Player.DOAnchorPosY(-245, 0.3f);
    }

    public void ShrinkPlayer()
    {
        mp3Player.DOAnchorPosY(-35, 0.3f);
    }

    public void EnterPauseButton()
    {
        pauseButton.DOScale(1.3f, 0.3f);
    }

    public void ExitPauseButton()
    {
        pauseButton.DOScale(1f, 0.3f);
    }

    public void EnterSkipButton()
    {
        forwardButton.DOScale(1.3f, 0.3f);
    }

    public void ExitSkipButton()
    {
        forwardButton.DOScale(1f, 0.3f);
    }
}
