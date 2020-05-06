using UnityEngine;


public static class CustomDebug 
{
    #region Fields

    public static bool IsDebug;

    #endregion


    #region Methods

    public static void Log(object value)
    { 
        if (IsDebug)
        {
            Debug.Log(value);
        }
    }

    #endregion
}
