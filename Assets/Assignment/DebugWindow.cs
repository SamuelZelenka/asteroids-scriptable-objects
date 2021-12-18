using UnityEngine;
using UnityEditor;


public class DebugWindow : EditorWindow
{
    private static Vector2 _scrollPos;
    private DebuggerDataScriptableObject debuggerData;


    [MenuItem("Debugger/DebugWindow")]
    private static void Init()
    {
        DebugWindow window = (DebugWindow)GetWindow(typeof(DebugWindow), true);
        window.Show();
    }

    private void OnGUI()
    {
        debuggerData = EditorGUILayout.ObjectField(debuggerData, typeof(DebuggerDataScriptableObject) ,true) as DebuggerDataScriptableObject;
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        debuggerData?.ShowContent();

        EditorGUILayout.EndScrollView();
    }
}