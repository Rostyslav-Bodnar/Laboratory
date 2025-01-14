
public class SetBoolInAnimator
{
    public string nameOfBool;
    public bool state;

    public SetBoolInAnimator(string _nameOfBool, bool _state)
    {
        this.nameOfBool = _nameOfBool;
        this.state = _state;
    }
}

public class SetTriggerInAnimator
{
    public string nameOfTrigger;

    public SetTriggerInAnimator(string nameOfTrigger)
    {
        this.nameOfTrigger = nameOfTrigger;
    }
}

public class SetRootMotionInAnimator
{
    public bool applyRootMotion;

    public SetRootMotionInAnimator(bool applyRootMotion)
    {
        this.applyRootMotion = applyRootMotion;
    }
}

public class SetFloatInAnimator
{
    public string nameOfFloat;
    public float value;
    public SetFloatInAnimator(string nameOfFloat, float value)
    {
        this.nameOfFloat = nameOfFloat;
        this.value = value;
    }
}
