using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerLocationController), true)]
public class PlayerLocationControllerEditor : Editor
{
    private PlayerLocationController _editorEntity;

    private void Awake()
    {
        _editorEntity = (PlayerLocationController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Перейти в текущую локацию"))
        {
            _editorEntity.SetCurrentLocation();
        }
    }
}
