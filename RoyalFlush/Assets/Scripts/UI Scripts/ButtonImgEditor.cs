using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ButtonImg))]
public class ButtonImgEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ButtonImg targetButtonImg = (ButtonImg)target;

        targetButtonImg.text = (GameObject)EditorGUILayout.ObjectField("text", targetButtonImg.text, typeof(GameObject), true);

        // Draw the original GUI element's inspector
        base.OnInspectorGUI();

        // Apply modified properties to mark the component as dirty.
        // Reference: https://docs.unity3d.com/ScriptReference/EditorUtility.SetDirty.html
        serializedObject.ApplyModifiedProperties();
    }
}
