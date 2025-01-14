using UnityEngine;

public class PlayerManager : CharacterManager
{
    public PlayerInputManager inputManager;
    public PlayerLocomotion playerLocomotion;

    public override void Init()
    {
        base.Init();

        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();

    }

    private void Update()
    {
        inputManager.HandleAllInput();
    }

}
