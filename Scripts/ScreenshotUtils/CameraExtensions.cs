using System;
using UnityEngine;

namespace DUCK.Utils.ScreenshotUtils
{
	public static class CameraExtensions
	{
		private const string INVALID_SIZE_MESSAGE = "width & height must both be above 0";

		/// <summary>
		/// Renders the camera's viewport into a texture
		/// </summary>
		/// <param name="camera">The camera to use</param>
		/// <param name="width">The width of the texture</param>
		/// <param name="height">The height of the texture</param>
		/// <param name="textureFormat">The texture format</param>
		/// <param name="cullingMask">The culling mask used during the render</param>
		/// <returns>A Texture2D object of the given width/height</returns>
		public static Texture2D RenderToTexture(this Camera camera, int width, int height,
			TextureFormat textureFormat = TextureFormat.ARGB32, int cullingMask = 0)
		{
			if (camera == null) throw new ArgumentNullException("camera");
			if (width <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);
			if (height <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);

			var renderTexture = new RenderTexture(width, height, 24);
			var screenShot = new Texture2D(width, height, textureFormat, false);

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
