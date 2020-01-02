using System;
using UnityEngine;

#if UNITY_2019_3_OR_NEWER
using UnityEngine.Experimental.Rendering;
#endif

namespace DUCK.Utils.ScreenshotUtils
{
	public static class CameraExtensions
	{
		private const string INVALID_SIZE_MESSAGE = "width & height must both be above 0";
#if UNITY_2019_3_OR_NEWER
		/// <summary>
		/// Renders the camera's viewport into a texture
		/// </summary>
		/// <param name="camera">The camera to use</param>
		/// <param name="width">The width of the texture</param>
		/// <param name="height">The height of the texture</param>
		/// <param name="cullingMask">The culling mask used during the render</param>
		/// <param name="graphicsFormat">The Render Texture format</param>
		/// <param name="antiAliasing">Anti Aliasing -- [1|2|4|8]</param>
		/// <returns>A Texture2D object of the given width/height</returns>
		public static Texture2D RenderToTexture(this Camera camera, int width, int height, int cullingMask = 0,
			GraphicsFormat graphicsFormat = GraphicsFormat.R32G32B32A32_SFloat,
			int antiAliasing = 8)
#else
		/// <summary>
		/// Renders the camera's viewport into a texture
		/// </summary>
		/// <param name="camera">The camera to use</param>
		/// <param name="width">The width of the texture</param>
		/// <param name="height">The height of the texture</param>
		/// <param name="cullingMask">The culling mask used during the render</param>
		/// <param name="textureFormat">The Texture format</param>
		/// <param name="renderTextureFormat">The Render Texture format</param>
		/// <param name="antiAliasing">Anti Aliasing -- [1|2|4|8]</param>
		/// <returns>A Texture2D object of the given width/height</returns>
		public static Texture2D RenderToTexture(this Camera camera, int width, int height, int cullingMask = 0,
			TextureFormat textureFormat = TextureFormat.ARGB32,
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.Default,
			int antiAliasing = 8)
#endif
		{
			if (camera == null) throw new ArgumentNullException(nameof(camera));
			if (width <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);
			if (height <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);

#if UNITY_2019_3_OR_NEWER
			var renderTexture = RenderTexture.GetTemporary(width, height, 24, graphicsFormat, antiAliasing);
			var screenShot = new Texture2D(width, height, graphicsFormat, TextureCreationFlags.None);
#else
			var renderTexture = RenderTexture.GetTemporary(width, height, 24,
				renderTextureFormat, RenderTextureReadWrite.Default, antiAliasing);
			var screenShot = new Texture2D(width, height, textureFormat, false);
#endif

			var prevCullingMask = camera.cullingMask;
			if (cullingMask > 0)
			{
				camera.cullingMask = cullingMask;
			}

			var prevTargetTexture = camera.targetTexture;

			camera.targetTexture = renderTexture;
			camera.Render();
			camera.targetTexture = prevTargetTexture;
			camera.cullingMask = prevCullingMask;

			RenderTexture.active = renderTexture;
			screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			screenShot.Apply();
			RenderTexture.active = null;

			renderTexture.Release();

			return screenShot;
		}
	}
}
