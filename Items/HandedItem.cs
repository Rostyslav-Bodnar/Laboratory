using UnityEngine;

public abstract class HandedItem : Item, IEquippable
{
    public bool isInHand;

    [Header("Action Animations")]
    public string[] nameOfAnimations;
}
