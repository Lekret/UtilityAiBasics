using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Gameplay.Stats.Editor
{
    [CustomEditor(typeof(StatsComponent))]
    public class StatsComponentEditor : UnityEditor.Editor
    {
        private readonly Dictionary<Type, (FieldInfo[] Fields, PropertyInfo[] Properties)> _typesData = new();

        public override bool RequiresConstantRepaint() => true;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var statsComponent = (StatsComponent) target;
            foreach (var stat in statsComponent.Stats)
            {
                var statType = stat.GetType();
                if (!_typesData.TryGetValue(statType, out var typeData))
                {
                    const BindingFlags bindingFlags = BindingFlags.Instance |
                                                      BindingFlags.Public |
                                                      BindingFlags.NonPublic;
                    typeData.Fields = statType.GetFields(bindingFlags);
                    typeData.Properties = statType.GetProperties(bindingFlags);
                    _typesData.Add(statType, typeData);
                }

                EditorGUILayout.LabelField($"[--{statType.Name}--]");

                foreach (var property in typeData.Properties)
                {
                    EditorGUILayout.LabelField($"{property.Name}: {property.GetValue(stat)}");
                }

                foreach (var field in typeData.Fields)
                {
                    EditorGUILayout.LabelField($"{field.Name}: {field.GetValue(stat)}");
                }

                EditorGUILayout.Space();
            }
        }
    }
}