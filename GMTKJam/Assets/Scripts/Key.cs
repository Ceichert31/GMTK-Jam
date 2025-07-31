using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    public string ID
    {
        get => keyID;
    }

    [SerializeField]
    private string keyID;

    public void Collect()
    {
        //Add object to player

        //Destroy object
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}

public interface ICollectable
{
    public string ID { get; }
    public void Collect();
    public void Delete();
}
