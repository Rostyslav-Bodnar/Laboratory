using UnityEngine;

public class AlertBarModel
{
    public float alertBarValue = 0f;
    public float detectionBarValue = 0f;

    public readonly float alertFillSpeed = 0.1f; // Ўвидк≥сть заповненн€ alertBar
    public readonly float detectionFillSpeed = 0.1f; // Ўвидк≥сть заповненн€ detectionBar

    private UI_AlertBar alertBar;
    public AlertBarPresenter alertBarPresenter;

    public AlertBarModel(UI_AlertBar alertBar)
    {
        this.alertBar = alertBar;
    }

    public void FillAlertBar()
    {
        if(alertBarValue < 1f)
        {
            alertBarValue += alertFillSpeed * Time.deltaTime;
            alertBar.FillAlertBar(alertBarValue);
        }

    }

    public void FillDetectionBar()
    {
        if(detectionBarValue < 1f)
        {
            detectionBarValue += detectionFillSpeed * Time.deltaTime;
            alertBar.FillDetectionBar(detectionBarValue);
        }
    }

    public void ReduceAlertBar()
    {
        if(alertBarValue > 0f)
        {
            alertBarValue -= alertFillSpeed * Time.deltaTime;
            alertBar.ReduceAlertBar(alertBarValue);
        }
    }
    public void ReduceDetectionBar()
    {
        if (detectionBarValue > 0f)
        {
            detectionBarValue -= detectionFillSpeed * Time.deltaTime;
            alertBar.ReduceDetectionBar(detectionBarValue);
        }
    }

}
