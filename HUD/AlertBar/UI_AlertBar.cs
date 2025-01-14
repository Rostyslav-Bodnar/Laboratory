using UnityEngine;
using UnityEngine.UI;

public class UI_AlertBar : MonoBehaviour
{
    [SerializeField] private Image alertBar;
    [SerializeField] private Image detectionBar;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        alertBar.fillAmount = 0;
        detectionBar.fillAmount = 0;
    }


    public void FillAlertBar(float value)
    {
        alertBar.fillAmount = value;
    }

    public void FillDetectionBar(float value)
    {
        detectionBar.fillAmount = value;
    }

    public void ReduceAlertBar(float value)
    {
        alertBar.fillAmount = value;
    }

    public void ReduceDetectionBar(float value)
    {
        detectionBar.fillAmount = value;
    }
}
