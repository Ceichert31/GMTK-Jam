using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        //Add object to player

        //Destroy object
    }
}

public interface ICollectable
{
    public void Collect();
}
