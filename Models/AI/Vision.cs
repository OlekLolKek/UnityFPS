using UnityEngine;


[System.Serializable]
public sealed class Vision
{
    #region Fields

    public float ActiveDis = 15;
    public float ActiveAng = 360;

    #endregion


    #region Methods

    public bool VisionM(Transform player, Transform target)
    {
        return Distance(player, target) && Angle(player, target) && !CheckBlocked(player, target);
    }

    private bool CheckBlocked(Transform player, Transform target)
    {
        if (!Physics.Linecast(player.position, target.position, out var hit)) return true;
        return hit.transform != target;
    }

    private bool Angle(Transform player, Transform target)
    {
        var angle = Vector3.Angle(target.position - player.position, player.forward);
        return angle <= ActiveAng;
    }

    private bool Distance(Transform player, Transform target)
    {
        return (player.position - target.position).sqrMagnitude <= ActiveDis * ActiveDis;
    }

    #endregion
}
