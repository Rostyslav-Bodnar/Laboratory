
public class PlayAnimationSignal
{
    public string nameOfAnimation;
    public bool isInteracting;
    public bool applyRootMotion;
    public bool canRotate;
    public bool canMove;

    public PlayAnimationSignal(string _nameOfAnimation, bool _isInteracting, bool _applyRootMotion = true, bool _canRotate = false, bool _canMove = false)
    {
        this.nameOfAnimation = _nameOfAnimation;
        this.isInteracting = _isInteracting;
        this.applyRootMotion = _applyRootMotion;
        this.canRotate = _canRotate;
        this.canMove = _canMove;
    }
}
