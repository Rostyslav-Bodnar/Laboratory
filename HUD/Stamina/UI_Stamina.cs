using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stamina : MonoBehaviour, IService
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider easeStaminaSlider;
    private Player player;
    private EventBus _eventBus;

    private float lerpSpeed = 0.05f;
    private Coroutine staminaCoroutine;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        player = ServiceLocator.Current.Get<Player>();
        staminaSlider = GetComponent<Slider>();
        staminaSlider.maxValue = player.playerStats.staminaController.maxStamina;
        staminaSlider.value = staminaSlider.maxValue;

        easeStaminaSlider.maxValue = staminaSlider.maxValue;
        easeStaminaSlider.value = easeStaminaSlider.maxValue;

        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnStaminaChanged>(ChangeStamina, 0);
    }

    private void ChangeStamina(OnStaminaChanged signal)
    {
        staminaSlider.value += signal.value;

        if (staminaCoroutine != null)
        {
            StopCoroutine(staminaCoroutine);
        }

        staminaCoroutine = StartCoroutine(ChangeStaminaCoroutine(staminaSlider.value));
    }

    private IEnumerator ChangeStaminaCoroutine(float targetValue)
    {
        while (Mathf.Abs(easeStaminaSlider.value - targetValue) > 0.01f)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, targetValue, lerpSpeed);
            yield return null;
        }

        easeStaminaSlider.value = targetValue;
    }
}
