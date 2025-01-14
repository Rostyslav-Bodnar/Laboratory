using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionServiceLocator : MonoBehaviour
{    
    private PlayerControls playerControls;

    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerLocomotion locomotion;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAttacker playerAttacker;

    private PlayerInputInit inputInit;
    private MovementCalculation movementCalculation;
    private MovementAnimationCalculation movementAnimationCalculation;

    private RotationLocomotion rotationLocomotion;
    private FallingLocomotion fallingLocomotion;

    private EventBus _eventBus;

    [SerializeField] private UI_Stamina stamina;

    private Player player;

    private void Awake()
    {
        rotationLocomotion = new RotationLocomotion();
        fallingLocomotion = new FallingLocomotion();
        _eventBus = new EventBus();
        inputInit = new PlayerInputInit();
        movementCalculation = new MovementCalculation();
        movementAnimationCalculation = new MovementAnimationCalculation();
        player = new Player();
        Init();

        animatorManager.Init();
        playerInputManager.Init();
        playerManager.Init();
        locomotion.Init();
        inputInit.Init();
        movementCalculation.Init();
        movementAnimationCalculation.Init();
        rotationLocomotion.Init();
        fallingLocomotion.Init();
        playerAttacker.Init();
        player.Init();
        stamina.Init();
    }
    public void Init()
    {
        
        playerControls ??= new PlayerControls();

        ServiceLocator.Init();

        Register();
    }

    private void Register()
    {
        ServiceLocator.Current.Register(_eventBus);
        ServiceLocator.Current.Register(locomotion);
        ServiceLocator.Current.Register(playerManager);
        ServiceLocator.Current.Register(playerInputManager);
        ServiceLocator.Current.Register(animatorManager);
        ServiceLocator.Current.Register(movementCalculation);
        ServiceLocator.Current.Register(movementAnimationCalculation);
        ServiceLocator.Current.Register(player);
        ServiceLocator.Current.Register(stamina);
        ServiceLocator.Current.Register(playerAttacker);
    }
}
