using UnityEngine;


public class MiniMap : MonoBehaviour
{
    #region Fields

    private Transform _player;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _player = Camera.main.transform;
        transform.parent = null;

        var rt = Resources.Load<RenderTexture>("Minimap");

        GetComponent<Camera>().targetTexture = rt;
    }

    private void LateUpdate()
    {
        var newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
    }

    #endregion
}
