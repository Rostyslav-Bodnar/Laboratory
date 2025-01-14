using UnityEngine;

public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public string nameOfItem;
    public string descriptionOfItem;
    public int maxAmount;

    public int stackSize;
}
