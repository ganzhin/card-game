using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender
{
	[ExecuteInEditMode]
	[InitializeOnLoad]
	public class RunMenuButton
	{
		private const string MainScenePath = @"Assets\card-game\Scenes\Menu\MainMenu.unity";

		static RunMenuButton()
		{
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if (GUILayout.Button(new GUIContent("Run", "Run game from Scene 0"), ToolbarStyles.commandButtonStyle))
			{
				if (!EditorApplication.isPlaying)
                {
					if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
						EditorApplication.isPlaying = true;

						return;
                    }
					
					if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
						try
						{
							EditorSceneManager.OpenScene(MainScenePath);
							EditorApplication.isPlaying = true;

						}
						catch
                        {
							Debug.LogError($"Can't load scene {MainScenePath}");
							EditorApplication.isPlaying = false;
                        }
                    }
                }
			}
		}
	}

	static class ToolbarStyles
	{
		public static readonly GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 12,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}
}