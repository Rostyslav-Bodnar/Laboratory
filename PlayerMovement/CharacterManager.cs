using UnityEngine;

public class CharacterManager : MonoBehaviour, IService
{
    public bool isInteracting;
    public bool canRotate = true;
    public bool canMove = true;
    public virtual void Init()
    {

    }
}
