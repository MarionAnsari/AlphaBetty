using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(AlphabetData))]
[CanEditMultipleObjects]
[System.Serializable]
public class AlphabetDataDrawer : Editor
{
    private ReorderableList _alphabetWhiteList;
    private ReorderableList _alphabetBlueList;
    private ReorderableList _alphabetGreenList;
    private ReorderableList _alphabetRedList;

    private void OnEnable()
    {
        InitializeReorderableList(ref _alphabetWhiteList, "alphabetWhite", "alphabet White Normal");
        InitializeReorderableList(ref _alphabetBlueList, "alphabetBlue", "alphabetBlue Dragged");
        InitializeReorderableList(ref _alphabetGreenList, "alphabetGreen", "alphabetGreen Correct");
        InitializeReorderableList(ref _alphabetRedList, "alphabetRed", "alphabetRed Wrong");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _alphabetWhiteList.DoLayoutList();
        _alphabetBlueList.DoLayoutList();
        _alphabetGreenList.DoLayoutList();
        _alphabetRedList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void InitializeReorderableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true,
            true);
        //lambda
        list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, listLabel);

        var alphabetList = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = alphabetList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("letterName"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 70, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("letterImage"), GUIContent.none);

        };
    }
}
