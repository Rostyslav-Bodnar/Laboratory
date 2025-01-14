using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float pickUpRadius = 1f;
    public Item ItemData;

    public SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = pickUpRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();
        if (!inventory) return;

        if (ItemData is IEquippable)
        {
            if (inventory.EquippableInventorySystem.AddToInventory(ItemData, 1))
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (inventory.NonEquippableInventorySystem.AddToInventory(ItemData, 1))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
