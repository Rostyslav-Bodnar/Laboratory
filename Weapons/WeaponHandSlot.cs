using UnityEngine;

public class WeaponHandSlot : MonoBehaviour
{
    [SerializeField] private Transform weaponHandler;
    [SerializeField] private Transform sheath;

    [SerializeField] public GameObject currentWeaponModel;
    [SerializeField] private DamageCollider damageCollider;

    private void UnloadWeapon()
    {
        if(currentWeaponModel != null)
            currentWeaponModel.SetActive(false);
    }

    public void UnloadWeaponAndDestroy()
    {
        if(currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }    
    }

    private void LoadWeaponModel(HandedItem weapon)
    {
        UnloadWeaponAndDestroy();
        if(weapon == null)
        { 
            UnloadWeapon();
            return;
        }

        GameObject model = Instantiate(weapon.prefab);

        if(model != null)
        {
            if(weaponHandler != null)
            {
                model.transform.parent = sheath;
            }
            else
            {
                model.transform.parent = transform;
            }
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            //currentWeaponModel.transform.localScale = Vector3.one;
        }
        currentWeaponModel = model;
    }

    public void LoadWeaponOnSlot(HandedItem weapon)
    {
        LoadWeaponModel(weapon);
        LoadDamageCollider();
    }

    public void DrawWeapon()
    {
        if (currentWeaponModel == null)
            return;
        currentWeaponModel.transform.parent = weaponHandler;
        currentWeaponModel.transform.localPosition = Vector3.zero;
        currentWeaponModel.transform.localRotation = Quaternion.identity;
    }
    public void SheathWeapon()
    {
        if (currentWeaponModel == null)
            return;
        currentWeaponModel.transform.parent = sheath;
        currentWeaponModel.transform.localPosition = Vector3.zero;
        currentWeaponModel.transform.localRotation = Quaternion.identity;
    }

    public void LoadDamageCollider()
    {
        damageCollider = currentWeaponModel?.GetComponentInChildren<DamageCollider>();
    }
    public void OpenDamageCollider()
    {
        damageCollider.EnableDamageCollider();
    }
    public void CloseDamageCollider()
    {
        damageCollider.DisableDamageCollider();
    }
}
