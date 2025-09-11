using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class ToolBox
    {
        [MenuItem("Tools/Scenes/Bootstrap &1", priority = 202)]
        public static void OpenBootstrapScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/0.Bootstrap.unity");
        }

        [MenuItem("Tools/Scenes/Battle &2", priority = 202)]
        public static void OpenCoreScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/1.Battle.unity");
        }
    }
}