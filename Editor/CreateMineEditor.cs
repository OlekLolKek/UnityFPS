#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;


[CustomEditor(typeof(CreateMine))]
public class CreateMineEditor : UnityEditor.Editor
{
    #region Fields

    private CreateMine _testTarget;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        _testTarget = (CreateMine)target;
    }

    private void OnSceneGUI()
    {
        if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
        {
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
                SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

            if (Physics.Raycast(ray, out var hit))
            {
                _testTarget.InstantiateObj(new Vector3(hit.point.x, hit.point.y + 0.05f, hit.point.z));
                SetObjectDirty(_testTarget.gameObject);
            }
        }
        Selection.activeGameObject = _testTarget.gameObject;
    }

    #endregion


    #region Methods

    public void SetObjectDirty(GameObject obj)
    {
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }
    }

    #endregion
}
#endif

