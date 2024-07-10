#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VinTools.BetterRuleTiles;

namespace VinToolsEditor.BetterRuleTiles
{
    [CustomEditor(typeof(BetterRuleTile))]
    public class BetterRuleTileInspector : RuleTileEditor
    {
        bool showInspector = false;

        public override void OnInspectorGUI()
        {
            if (!showInspector)
            {
                EditorGUILayout.HelpBox(
                    "This asset was not intended to be edited by users. " +
                    "This asset was generated automatically, therefore there are settings which can be confusing, and which after changed cannot be changed back. " +
                    "Only use this panel for debugging purposes. " +
                    "Changes made in the asset will be lost when generating a new asset.", 
                    MessageType.Warning);
                EditorGUILayout.Space();
                if (GUILayout.Button("Display inspector")) showInspector = true;
            }

            if (showInspector) base.OnInspectorGUI();
        }
    }
}
#endif