using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour, IService
{
    private EventBus _eventBus;
    [SerializeField] private int attackCounter = 0;
    [SerializeField] private bool _isAttacking = false;

    public bool test = false;

    [SerializeField] private PlayerInventory playerWeaponInventory;

    [SerializeField] private Shield leftArmWeapon;
    [SerializeField] private Weapon weaponOnHand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            test = !test;
            _eventBus.Invoke(new SetBoolInAnimator("isEnemyVisible", test));
        }
    }

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnPlayerDefence>(HandleLeftArmAttack);
        _eventBus.Subscribe<OnPlayerRightArmAttack>(HandleArmAttack);
    }

    private void HandleLeftArmAttack(OnPlayerDefence signal)
    {
        if (leftArmWeapon == null || !leftArmWeapon.isInHand)
            return;
        HandleLeftArmAction(leftArmWeapon);
    }

    private void HandleArmAttack(OnPlayerRightArmAttack signal)
    {
        if(weaponOnHand == null || !weaponOnHand.isInHand)
            return;
        HandleRightArmAction(weaponOnHand);
    }

    private void HandleLeftArmAction(Shield shield)
    {
        Debug.Log("Defence");
        if (shield == null || !shield.isInHand)
            return;

        _eventBus.Invoke(new SetBoolInAnimator("isBlocking", true));
        _eventBus.Invoke(new PlayAnimationSignal(shield.nameOfAnimations[0], false, false, true, true));
    }
    private void HandleRightArmAction(Weapon weapon)
    {
        _eventBus.Invoke(new SetBoolInAnimator("isEnemyVisible", true));
        _eventBus.Invoke(new SetBoolInAnimator("isBlocking", false));
        if (!weapon.isInHand)
            return;
        if (_isAttacking)
            return;
        _isAttacking = true;
        if (attackCounter >= weapon.nameOfAnimations.Length)
            attackCounter = 0;
        _eventBus.Invoke(new PlayAnimationSignal(weapon.nameOfAnimations[attackCounter], true, true));
    }

    public void InitLeftWeapon(Shield weapon)
    {
        leftArmWeapon = weapon;
    }
    public void InitWeapon(Weapon weapon)
    {
        weaponOnHand = weapon;
        _eventBus.Invoke(new SetAnimatorControllerSignal(weaponOnHand.weaponAnimator));
    }

    public void UninitializeWeapon()
    {
        weaponOnHand = null;
    }


    #region Animation Events
    public void SetAttackingBool()
    {
        _isAttacking = false;
    }

    public void AllowComboAttack()
    {
        _isAttacking = false;
        attackCounter++;
    }

    public void ForbidComboAttack()
    {
        _isAttacking = false;
        attackCounter = 0;
    }

    #endregion

}
