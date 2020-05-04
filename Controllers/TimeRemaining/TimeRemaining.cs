using System;

public class TimeRemaining : ITimeRemaining
{
    #region Properties

    public Action Method { get; }
    public bool IsRepeating { get; }
    public float Time { get; }
    public float CurrentTime { get; set; }


    #endregion


    #region ClassLyfeCycles

    public TimeRemaining(Action method, float time, bool isRepeating = false)
    {
        Method = method;
        Time = time;
        CurrentTime = time;
        IsRepeating = isRepeating;
    }

    #endregion
}
