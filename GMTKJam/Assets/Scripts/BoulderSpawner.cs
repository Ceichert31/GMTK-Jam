using DG.Tweening;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnChance = 5;

    private bool isSpawning;

    [SerializeField]
    private float warningTime = 5f;

    [SerializeField]
    private SpriteRenderer warningSprite;

    private Vector3 cameraOffset = new Vector3(0, 5, 0);

    private void Update()
    {
        int chance = Random.Range(0, 10000);
        if (spawnChance < chance && !isSpawning)
        {
            isSpawning = true;
            //Show exclemation point
            //Dont spawn more boulders
            //Spawn boulders
            warningSprite.DOFade(1, 1f);
            Tweener tween = warningSprite
                .transform.DOMove(
                    GameManager.Instance.PlayerPosition.position + cameraOffset,
                    warningTime
                )
                .OnComplete(() => {
                    //Spawn rock
                });

            tween.Play();

            tween.OnUpdate(() =>
            {
                tween.ChangeEndValue(GameManager.Instance.PlayerPosition.position + cameraOffset);
            });
        }
    }
}
