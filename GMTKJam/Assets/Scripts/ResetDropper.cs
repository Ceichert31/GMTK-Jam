using UnityEngine;

public class ResetDropper : MonoBehaviour, IResetable
{
    [SerializeField]
    private GameObject bottomCrystal;

    [SerializeField]
    private Transform bottomCrystalOriginalPos;

    [SerializeField]
    private GameObject topCrystal;

    [SerializeField]
    private GameObject arrow;

    public void Reset()
    {
        bottomCrystal.SetActive(true);
        bottomCrystal.transform.parent = bottomCrystalOriginalPos;
        bottomCrystal.transform.position = Vector2.zero;
        topCrystal.SetActive(false);
        arrow.transform.localScale.Set(
            arrow.transform.localScale.x,
            0,
            arrow.transform.localScale.z
        );
    }
}

public interface IResetable
{
    public void Reset();
}
