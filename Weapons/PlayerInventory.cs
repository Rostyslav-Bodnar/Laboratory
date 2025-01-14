using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] public EquippedSlotScript[] equippedSlots;
    [SerializeField] public PlayerInputManager inputManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            obj.SetActive(!obj.activeSelf);
            inputManager.canDoAction = !obj.activeSelf;

            if (!inputManager.canDoAction)
            {
                inputManager.ResetInputs();
            }
        }
    }
}
