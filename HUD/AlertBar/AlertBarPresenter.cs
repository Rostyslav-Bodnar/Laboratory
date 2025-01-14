
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
        // ���������� alertBar
        if (alertBarModel.alertBarValue < 1f)
        {
            alertBarModel.FillAlertBar();
        }

        // ���������� detectionBar, ���� alertBar ���������
        if (alertBarModel.alertBarValue >= 1f && alertBarModel.detectionBarValue < 1f)
        {
            alertBarModel.FillDetectionBar();
        }
    }


    public void ReduceAlertBar()
    {
        // ���������� alertBar
        if (alertBarModel.detectionBarValue >= 1f)
        {
            alertBarModel.ReduceDetectionBar();
        }

        // ���������� detectionBar, ���� alertBar ���������
        if (alertBarModel.alertBarValue >= 1f && alertBarModel.detectionBarValue <= 0f)
        {
            alertBarModel.ReduceAlertBar();
        }
    }

}
