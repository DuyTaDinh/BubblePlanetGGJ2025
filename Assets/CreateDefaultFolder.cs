#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CreateDefaultFolder : EditorWindow
{
	private string projectName = "YourProjectName";
	private List<string> _defaultFolderPaths = new List<string>()
	{
		"Animations",
		"Art/Materials",
		"Art/Models",
		"Art/Effects",
		"Art/UI",
		"Art/UI/Textures",
		"Audio/Music",
		"Audio/SFX",
		"Fonts",
		"Documents",
		"GameConfigs",
		"Prefabs/UI",
		"Resources",
		"Scenes",
		"Scripts/Managers",
		"Scripts/Gameplay",
		"Scripts/UI",
		"Scripts/Combat",
		"Scripts/Editor",
		"Scripts/Shaders",
		"Scripts/Utilities",
		"Scripts/Plugins",
		"Settings",
		"ThirdParty"
	};

	private Vector2 _scrollPosition;
	private string _newFolderPath = "";

	[MenuItem("Tools/Create Default Folder")]
	public static void ShowWindow()
	{
		GetWindow<CreateDefaultFolder>("Create Default Folder");
	}

	void OnEnable()
	{
		_defaultFolderPaths.Sort();
		_defaultFolderPaths.Reverse();
	}

	private void OnGUI()
	{
		GUILayout.Space(10);

		// Project Name Section
		EditorGUILayout.BeginVertical("box");
		GUILayout.Label("Project Settings", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Project Name:", GUILayout.Width(100));
		projectName = EditorGUILayout.TextField(projectName);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		GUILayout.Space(10);

		// Folder Structure Section
		EditorGUILayout.BeginVertical("box");
		GUILayout.Label("Folder Structure", EditorStyles.boldLabel);

		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
		for (int i = _defaultFolderPaths.Count - 1; i >= 0; i--) // Iterate in reverse to safely remove item
		{
			EditorGUILayout.BeginHorizontal();

			_defaultFolderPaths[i] = EditorGUILayout.TextField(_defaultFolderPaths[i]);

			if (GUILayout.Button("Delete", GUILayout.Width(60)))
			{
				_defaultFolderPaths.RemoveAt(i);
			}

			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();

		GUILayout.FlexibleSpace();
		GUILayout.Space(10);

		// Add New Folder Section
		EditorGUILayout.BeginHorizontal("box");
		_newFolderPath = EditorGUILayout.TextField("New Folder Path:", _newFolderPath);

		if (GUILayout.Button("Add Folder", GUILayout.Width(100)))
		{
			GUI.FocusControl(null);
			if (!string.IsNullOrEmpty(_newFolderPath) && !_defaultFolderPaths.Contains(_newFolderPath))
			{
				_defaultFolderPaths.Add(_newFolderPath);
				_newFolderPath = "";
				_defaultFolderPaths.Sort();
				_defaultFolderPaths.Reverse();
			}
		}
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(10);

		if (GUILayout.Button("Generate Default Folder", GUILayout.Height(40)))
		{
			GenerateFolders();
		}
	}

	private void GenerateFolders()
	{
		foreach (string folderPath in _defaultFolderPaths)
		{
			string fullPath = Path.Combine(Application.dataPath, projectName, folderPath);
			// Debug.Log(fullPath);
			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}
		}
		string assetFolderPath = Path.Combine(Application.dataPath, "Plugins");
		if (!Directory.Exists(assetFolderPath))
		{
			Directory.CreateDirectory(assetFolderPath);
		}
		AssetDatabase.Refresh();
		Debug.Log("Default folder structure generated successfully!");
	}
}
#endif