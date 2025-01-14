using UnityEngine;

public class ArmorSlot : MonoBehaviour
{
    [SerializeField] private Transform parent;

    [SerializeField] private GameObject armorModel;

    private void UnloadArmor()
    {
        if (armorModel != null)
            armorModel.SetActive(false);
    }

    private void UnloadArmorAndDestroy()
    {
        if (armorModel != null)
        {
            Destroy(armorModel);
        }
    }

    public void LoadArmorModel(Armor armor)
    {
        UnloadArmorAndDestroy();
        if (armor == null)
        {
            UnloadArmor();
            return;
        }

        GameObject model = Instantiate(armor.prefab);

        if (model != null)
        {
            if (parent != null)
            {
                model.transform.parent = parent;
            }
            else
            {
                model.transform.parent = transform;
            }
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            //currentWeaponModel.transform.localScale = Vector3.one;
        }
        armorModel = model;
    }

    public void LoadArmorOnSlot(Armor armor)
    {
        LoadArmorModel(armor);
    }
}
