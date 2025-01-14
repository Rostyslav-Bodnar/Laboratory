
using UnityEngine;

public class AlertBarPresenter
{
    public AlertBarModel alertBarModel;

    public AlertBarPresenter(UI_AlertBar alertBar)
    {
        this.alertBarModel = new AlertBarModel(alertBar);
    }

    public void FillAlertBar()
    {
        // Заповнюємо alertBar
        if (alertBarModel.alertBarValue < 1f)
        {
            alertBarModel.FillAlertBar();
        }

        // Заповнюємо detectionBar, коли alertBar заповнено
        if (alertBarModel.alertBarValue >= 1f && alertBarModel.detectionBarValue < 1f)
        {
            alertBarModel.FillDetectionBar();
        }
    }


    public void ReduceAlertBar()
    {
        // Заповнюємо alertBar
        if (alertBarModel.detectionBarValue >= 1f)
        {
            alertBarModel.ReduceDetectionBar();
        }

        // Заповнюємо detectionBar, коли alertBar заповнено
        if (alertBarModel.alertBarValue >= 1f && alertBarModel.detectionBarValue <= 0f)
        {
            alertBarModel.ReduceAlertBar();
        }
    }

}
