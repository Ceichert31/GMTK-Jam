using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MP3UIController : MonoBehaviour
{
    //Update song duration, icon, name, and progress bar

    [SerializeField]
    private Image fillBar;

    [SerializeField]
    private Image songIcon;

    [SerializeField]
    private TextMeshProUGUI songLengthText;

    [SerializeField]
    private TextMeshProUGUI songNameText;

    private float songTimer;
    private float songLength;

    private void Update()
    {
        songTimer += Time.deltaTime;
        fillBar.fillAmount = songTimer / songLength;
        songLengthText.text = Mathf.RoundToInt(songTimer).ToString();

        if (songTimer >= songLength)
        {
            ResetUI();
        }
    }

    private void ResetUI()
    {
        //Reset song timer
        songTimer = 0;
        fillBar.fillAmount = 0;
    }

    public void UpdateSongInfo(SongEvent ctx)
    {
        ResetUI();
        songNameText.text = ctx.SongValue.songName;
        songLength = ctx.SongValue.songFile.length;
        songIcon.sprite = ctx.SongValue.songIcon;
    }
}
