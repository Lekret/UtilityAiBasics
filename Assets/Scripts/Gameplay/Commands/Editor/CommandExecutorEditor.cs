using UnityEditor;

namespace Gameplay.Commands.Editor
{
    [CustomEditor(typeof(CommandExecutor))]
    public class CommandExecutorEditor : UnityEditor.Editor
    {
        public override bool RequiresConstantRepaint() => true;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var commandExecutor = (CommandExecutor) target;
            var currentCommand = commandExecutor.CurrentCommand;
            var commandName = currentCommand != null ? currentCommand.GetType().Name : "(null)";
            EditorGUILayout.LabelField($"CurrentCommand: {commandName}");
        }
    }
}