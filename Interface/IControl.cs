public interface IControl
{
    #region Properties

    UnityEngine.GameObject Instance { get; }
    UnityEngine.UI.Selectable Control { get; }

    #endregion
}


public interface IControlText : IControl
{
    #region Properties

    UnityEngine.UI.Text GetText { get; }

    #endregion
}


public interface IControlImage : IControl
{
    #region Properties

    UnityEngine.UI.Image GetImage { get; }

    #endregion
}
