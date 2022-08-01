/*
 * @author Marty Fitzer
 */

using UnityEditor;
using UnityEngine;

namespace KonnectVR.Interactions.Connectables
{
    public class ConnectableAlignmentTool : EditorWindow
    {
        private Connectable connector;
        private Connectable receiver;

        [MenuItem("Window/Connectable Alignment Tool")]
        public static void ShowWindow()
        {
            GetWindow<ConnectableAlignmentTool>("Connectable Alignment Tool");
        }

        private void OnGUI()
        {
            connector = (Connectable)EditorGUILayout.ObjectField("Connector", connector, typeof(Connectable), true);
            receiver = (Connectable)EditorGUILayout.ObjectField("Receiver", receiver, typeof(Connectable), true);

            if (GUILayout.Button("Align Connectables"))
            {
                Undo.RecordObject(receiver.transform.parent, "Aligned Connectables");

                Quaternion snapRotation = connector.transform.rotation * Quaternion.Inverse(receiver.transform.rotation);
                receiver.transform.parent.rotation = snapRotation * receiver.transform.parent.rotation;

                //Determine how far to translate to snap into position
                Vector3 translation = connector.transform.position - receiver.transform.position;
                receiver.transform.parent.position = receiver.transform.parent.transform.position + translation;

            }
        }
    }
}