﻿public class BaseController
{
    #region Fields

    public bool IsActive { get; private set; }

    #endregion


    #region Methods

    public virtual void On()
    {
        On(null);
    }

    public virtual void On(params BaseObjectScene[] obj)
    {
        IsActive = true;
    }

    public virtual void Off()
    {
        IsActive = false;
    }

    public void Switch()
    {
        if (!IsActive)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    #endregion
}
