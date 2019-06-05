﻿using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

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
		/// <param name="cullingMask">The culling mask used during the render</param>
		/// <param name="textureFormat">The Texture format</param>
		/// <param name="renderTextureFormat">The Render Texture format</param>
		/// <returns>A Texture2D object of the given width/height</returns>
		public static Texture2D RenderToTexture(this Camera camera, int width, int height, int cullingMask = 0,
			TextureFormat textureFormat = TextureFormat.ARGB32,
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGBFloat,
			int antiAliasing = 8)
		{
			if (camera == null) throw new ArgumentNullException("camera");
			if (width <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);
			if (height <= 0) throw new ArgumentException(INVALID_SIZE_MESSAGE);

			var renderTexture = RenderTexture.GetTemporary(width, height, 24,
				renderTextureFormat, RenderTextureReadWrite.Default, antiAliasing);
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
