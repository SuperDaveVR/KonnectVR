using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InteractionSystem
{
    [CustomEditor(typeof(LocalRigidbodyConstraint)), CanEditMultipleObjects]
    public class LocalRigidbodyConstraintEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LocalRigidbodyConstraint constraint = (LocalRigidbodyConstraint) target;

            constraint.showConstraints = EditorGUILayout.Foldout(constraint.showConstraints, "Constraints");

            if (constraint.showConstraints)
            {
                #region Position Fields

                float originalLabelWidth = EditorGUIUtility.labelWidth;

                EditorGUILayout.BeginHorizontal("Box");

                EditorGUILayout.LabelField("Freeze Local Position", GUILayout.Width(140));

                EditorGUIUtility.labelWidth = 10;

                constraint.freezeXPosition = EditorGUILayout.Toggle("X", constraint.freezeXPosition, GUILayout.Width(30));
                constraint.freezeYPosition = EditorGUILayout.Toggle("Y", constraint.freezeYPosition, GUILayout.Width(30));
                constraint.freezeZPosition = EditorGUILayout.Toggle("Z", constraint.freezeZPosition, GUILayout.Width(30));

                EditorGUIUtility.labelWidth = originalLabelWidth;

                EditorGUILayout.EndHorizontal();

                #endregion Position Fields

                #region Rotation Fields

                EditorGUILayout.BeginHorizontal("Box");

                EditorGUILayout.LabelField("Freeze Local Rotation", GUILayout.Width(140));

                EditorGUIUtility.labelWidth = 10;

                constraint.freezeXRotation = EditorGUILayout.Toggle("X", constraint.freezeXRotation, GUILayout.Width(30));
                constraint.freezeYRotation = EditorGUILayout.Toggle("Y", constraint.freezeYRotation, GUILayout.Width(30));
                constraint.freezeZRotation = EditorGUILayout.Toggle("Z", constraint.freezeZRotation, GUILayout.Width(30));

                EditorGUIUtility.labelWidth = originalLabelWidth;

                EditorGUILayout.EndHorizontal();

                #endregion Position Fields
            }
        }
    }
}