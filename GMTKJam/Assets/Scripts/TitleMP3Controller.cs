using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMP3Controller : MonoBehaviour
{
    private const float SONG_LENGTH = 31.476f;

    private float songTimer;

    [SerializeField]
    private RectTransform fillBar;

    Vector2 startPos = new Vector2(-276, 3.300005f);
    Vector2 endPos = new Vector2(0, 3.300005f);

    private Coroutine instance;

    private void Start()
    {
        instance = StartCoroutine(MoveSongBar());
    }

    private IEnumerator MoveSongBar()
    {
        while (songTimer < SONG_LENGTH)
        {
            Vector2 intVector = Vector2.Lerp(startPos, endPos, songTimer / SONG_LENGTH);

            intVector.Set(Mathf.RoundToInt(intVector.x), Mathf.RoundToInt(intVector.y));

            fillBar.anchoredPosition = intVector;

            yield return null;
        }
        fillBar.anchoredPosition = endPos;
        instance = null;
        songTimer = 0;
    }

    private void Update()
    {
        songTimer += Time.deltaTime;

        if (instance != null)
            return;

        instance = StartCoroutine(MoveSongBar());
    }
}
