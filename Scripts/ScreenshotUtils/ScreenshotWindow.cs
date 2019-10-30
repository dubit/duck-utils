#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_2019_3_OR_NEWER
using UnityEngine.Experimental.Rendering;
#endif

namespace DUCK.Utils.ScreenshotUtils
{
	public class RenderToFile : EditorWindow
	{
		private enum FileFormat
		{
			PNG,
			JPEG
		}

		private enum AntiAliasing
		{
			None = 1,
			Two = 2,
			Four = 4,
			Eight = 8
		}

		private Camera renderCamera;
		private FileFormat fileFileFormat;
#if UNITY_2019_3_OR_NEWER
		private GraphicsFormat graphicsFormat = GraphicsFormat.R32G32B32A32_SFloat;
#else
		private TextureFormat textureFormat = TextureFormat.ARGB32;
		private RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGBFloat;
#endif
		private int jpegQuality = 75;
		private int width = 512;
		private int height = 512;
		private AntiAliasing antiAliasing = AntiAliasing.Eight;
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

#if UNITY_2019_3_OR_NEWER
			graphicsFormat = (GraphicsFormat)EditorGUILayout.EnumPopup("Graphics Format", graphicsFormat);
#else
			textureFormat = (TextureFormat)EditorGUILayout.EnumPopup("Texture Format", textureFormat);
			renderTextureFormat = (RenderTextureFormat)EditorGUILayout.EnumPopup("Render Texture Format", renderTextureFormat);
#endif

			EditorGUILayout.BeginHorizontal();
			width = EditorGUILayout.IntField("Width", width);
			height = EditorGUILayout.IntField("Height", height);
			EditorGUILayout.EndHorizontal();
			antiAliasing = (AntiAliasing)EditorGUILayout.EnumPopup("Anti Aliasing", antiAliasing);

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

#if UNITY_2019_3_OR_NEWER
				var texture = camera.RenderToTexture(width, height, 0, graphicsFormat, (int)antiAliasing);
#else
				var texture = camera.RenderToTexture(width, height, 0, textureFormat, renderTextureFormat, (int)antiAliasing);
#endif
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