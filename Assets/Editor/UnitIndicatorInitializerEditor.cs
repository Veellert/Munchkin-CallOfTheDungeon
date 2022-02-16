using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitIndicatorInitializer), true)]
public class UnitIndicatorInitializerEditor : Editor
{
    private UnitIndicatorInitializer _editorEntity;

    private void Awake()
    {
        _editorEntity = (UnitIndicatorInitializer)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(20);
        if (GUILayout.Button("���������� ���������"))
        {
            _editorEntity.InstantiateIndicator();
        }
        if (GUILayout.Button("�������� ���������"))
        {
            _editorEntity.ClearIndicator();
        }
    }
}
