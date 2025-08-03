using UnityEngine;

public class AlbumGrab : MonoBehaviour
{
    [SerializeField]
    float amplitude = 1f; // Height of the wave

    [SerializeField]
    float frequency = 1f; // Speed of the wave
    private Vector3 startPos;

    [SerializeField]
    VoidEventChannel complete;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            complete.CallEvent(new());
        }
    }
}
