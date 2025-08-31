using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FSMExample fsm = (FSMExample)target;
        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");
        if (fsm.stateMachine == null) return;
    }
}
