using UnityEngine;

[CreateAssetMenu(menuName = "Items/Armor Item")]
public class Armor : Item, IEquippable
{
    public float defence;
    public ArmorType armorType;

}

public enum ArmorType
{ 
    Helmet = 0,
    Breastplate = 1,
    Leggins = 2,
    Boots = 3,
    Hands = 4,
}

