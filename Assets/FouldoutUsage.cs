// Create a foldable menu that hides/shows the selected transform position.
// If no Transform is selected, the Foldout item will be folded until
// a transform is selected.

using UnityEditor;
using UnityEngine;

public class FouldoutUsage : EditorWindow
{
    bool showPosition = true;
    string status = "Select a GameObject";

    [MenuItem("Examples/Foldout Usage")]
    static void Init()
    {
        FouldoutUsage window = (FouldoutUsage)GetWindow(typeof(FouldoutUsage));
        window.Show();
    }

    public void OnGUI()
    {
        showPosition = EditorGUILayout.Foldout(showPosition, status);
        if (showPosition)
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                    EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;
            }

        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            showPosition = false;
        }
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
