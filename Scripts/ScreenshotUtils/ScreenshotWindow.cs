#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DUCK.Utils.ScreenshotUtils
{
	public class RenderToFile : EditorWindow
	{
		private enum FileFormat
		{
			PNG,
			JPEG,
		}

		private Camera renderCamera;
		private FileFormat fileFileFormat;
		private TextureFormat textureFormat = TextureFormat.ARGB32;
		private RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGBFloat;
		private int jpegQuality = 75;
		private int width = 512;
		private int height = 512;
		private int antiAliasing = 8;
		private bool nameRendersByDate;
		private string lastPath;

		[MenuItem("DUCK/Render to File/Show Window", false)]
		public static void ShowWindow()
		{
			GetWindow<RenderToFile>(false, "Render to File", true);
		}

		private void Awake()
		{
			EditorApplication.update += Repaint;
		}

		private void OnDestroy()
		{
			EditorApplication.update -= Repaint;
		}

		private void OnGUI()
		{
			Camera camera;

			renderCamera = EditorGUILayout.ObjectField("Render Camera", renderCamera, typeof(Camera), true) as Camera;

			if (renderCamera == null)
			{
				GUILayout.BeginHorizontal();
				GUI.enabled = Camera.main != null;
				if (GUILayout.Button("Main Camera"))
				{
					renderCamera = Camera.main;
				}
				GUI.enabled = true;
				if (GUILayout.Button("First Camera"))
				{
					renderCamera = FindObjectOfType<Camera>();
				}
				GUILayout.EndHorizontal();
			}

			if (renderCamera == null)
			{
				if (SceneView.lastActiveSceneView != null)
				{
					camera = SceneView.lastActiveSceneView.camera;
					EditorGUILayout.HelpBox("If you don't choose a render camera, the scene camera will be used. It is recommended to use a real camera", MessageType.Warning);
				}
				else
				{
					EditorGUILayout.HelpBox("No valid cameras found to render, please assign a camera or open a scene view", MessageType.Warning);
					return;
				}
			}
			else
			{
				camera = renderCamera;
			}

			fileFileFormat = (FileFormat)EditorGUILayout.EnumPopup("Format", fileFileFormat);
			if (fileFileFormat == FileFormat.JPEG)
			{
				RenderJPEGOptions();
			}

			textureFormat = (TextureFormat)EditorGUILayout.EnumPopup("Texture Format", textureFormat);
			renderTextureFormat = (RenderTextureFormat)EditorGUILayout.EnumPopup("Render Texture Format", renderTextureFormat);

			EditorGUILayout.BeginHorizontal();
			width = EditorGUILayout.IntField("Width", width);
			height = EditorGUILayout.IntField("Height", height);
			EditorGUILayout.EndHorizontal();
			antiAliasing = EditorGUILayout.IntField("Anti Aliasing (0, 1, 2, 4, 8)", antiAliasing);

			EditorGUILayout.Space();
			nameRendersByDate = EditorGUILayout.ToggleLeft("Name Renders By Date", nameRendersByDate);
			if (nameRendersByDate)
			{
				lastPath = EditorGUILayout.TextField("Output Directory", lastPath);
				if (GUILayout.Button("Choose Path"))
				{
					lastPath = EditorUtility.OpenFolderPanel(
						"Select Directory",
						string.IsNullOrEmpty(lastPath) ? Application.dataPath : lastPath, "Screenshots");
				}
			}

			EditorGUILayout.Space();
			if (GUILayout.Button("Render"))
			{
				string path = "";
				if (nameRendersByDate)
				{
					path = lastPath + "/Render_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
					path += fileFileFormat == FileFormat.PNG ? ".png" : ".jpeg";
				}
				else
				{
					var currentObj = Selection.activeObject;
					var filename = currentObj == null ? "Render" : currentObj.name;
					filename = filename.ToLower().Replace(' ', '_');

					path = EditorUtility.SaveFilePanel(
						"Save as",
						string.IsNullOrEmpty(lastPath) ? Application.dataPath : lastPath,
						filename,
						fileFileFormat == FileFormat.PNG ? "png" : "jpeg");

					lastPath = path;
				}

				Debug.Log("Outputting to: " + path);
				Debug.Log("Last Path: " + lastPath);

				if (string.IsNullOrEmpty(path))
				{
					return;
				}

				var texture = camera.RenderToTexture(width, height, 0, textureFormat, renderTextureFormat, antiAliasing);
				var imageData = fileFileFormat == FileFormat.PNG ? texture.EncodeToPNG() : texture.EncodeToJPG(jpegQuality);
				File.WriteAllBytes(path, imageData);
				DestroyImmediate(texture);

				AssetDatabase.Refresh();
			}
		}

		private void RenderJPEGOptions()
		{
			jpegQuality = EditorGUILayout.IntSlider("JPEG Quality", jpegQuality, 1, 100);
		}
	}
}
#endif