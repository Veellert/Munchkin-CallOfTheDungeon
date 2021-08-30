using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor
{
    private DungeonGenerator _generator;

    private void Awake()
    {
        _generator = (DungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Очистить подземелье"))
        {
            _generator.ClearDungeon();
        }
        if(GUILayout.Button("Отрисовать подземелье"))
        {
            _generator.VisualizeDungeon();
        }
        if(GUILayout.Button("Сгенерировать подземелье"))
        {
            _generator.GenerateDungeon();
        }
    }
}
