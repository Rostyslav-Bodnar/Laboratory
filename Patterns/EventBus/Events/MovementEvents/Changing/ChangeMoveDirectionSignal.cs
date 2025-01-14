using UnityEngine;

public class ChangeMoveDirectionSignal
{
    public Vector3 moveDirection;
    public float speed;

    public ChangeMoveDirectionSignal(float _speed)
    {
        this.speed = _speed;
    }
}
