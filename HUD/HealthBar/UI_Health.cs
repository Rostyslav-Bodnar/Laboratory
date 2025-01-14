using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    private Player player;
    private EventBus _eventBus;

    private float lerpSpeed = 0.05f;
    private Coroutine staminaCoroutine;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        player = ServiceLocator.Current.Get<Player>();
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = player.playerStats.healthController.maxHealth;
        healthSlider.value = healthSlider.maxValue;

        easeHealthSlider.maxValue = healthSlider.maxValue;
        easeHealthSlider.value = easeHealthSlider.maxValue;

        Subscribe();
    }

    private void Subscribe()
    {
        _eventBus.Subscribe<OnHealthChanged>(ChangeHealth, 0);
    }

    private void ChangeHealth(OnHealthChanged signal)
    {
        healthSlider.value += signal.value;

        if (staminaCoroutine != null)
        {
            StopCoroutine(staminaCoroutine);
        }

        staminaCoroutine = StartCoroutine(ChangeHealthCoroutine(healthSlider.value));
    }

    private IEnumerator ChangeHealthCoroutine(float targetValue)
    {
        while (Mathf.Abs(easeHealthSlider.value - targetValue) > 0.01f)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, targetValue, lerpSpeed);
            yield return null;
        }

        easeHealthSlider.value = targetValue;
    }
}
