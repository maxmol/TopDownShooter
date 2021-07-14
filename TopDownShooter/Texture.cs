using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using unvell.D2DLib;

namespace TopDownShooter
{
	public class Texture
	{
		private static class ImageLoader
		{
			private static Dictionary<string, D2DBitmap> Cache = new();
			private static Dictionary<string, D2DBitmap> CacheRegions = new();
			private static string ImagesFolder = "Assets/Sprites/";
			private static readonly D2DBitmap ErrorBitmap = LoadBitmap("error.png");

			public static D2DBitmap LoadBitmap(string path)
			{
				if (Cache.TryGetValue(path, out D2DBitmap cachedBitmap))
					return cachedBitmap;

				try
				{
					string fullPath = ImagesFolder + path;
					Bitmap bmp = new Bitmap(fullPath);

					D2DBitmap d2dBmp = Form1.Instance.Device.CreateBitmapFromGDIBitmap(bmp);
					Log.Info("Loaded " + fullPath);
					Cache[path] = d2dBmp;
					return d2dBmp;
				}
				catch (Exception e)
				{
					Log.Error("Error loading material " + path);
					Log.Warning(e.Message);
				}

				return ErrorBitmap;
			}

			public static D2DBitmap LoadBitmapRegion(string path, int x, int y, int w, int h)
			{
				string cacheKey = $"{path}[{x},{y},{w},{h}]";
				if (CacheRegions.TryGetValue(cacheKey, out D2DBitmap cachedBitmap))
					return cachedBitmap;

				Log.Info("Creating bitmap from region " + cacheKey);
				try
				{
					string fullPath = ImagesFolder + path;
					Bitmap bmp = new Bitmap(fullPath);

					Rectangle cut = new(x, y, w, h);
					bmp = bmp.Clone(cut, PixelFormat.DontCare);
					D2DBitmap tileBitmap = Form1.Instance.Device.CreateBitmapFromGDIBitmap(bmp);
					CacheRegions[cacheKey] = tileBitmap;
					return tileBitmap;
				}
				catch (Exception e)
				{
					Log.Error("Error loading material " + path);
					Log.Warning(e.Message);
				}
				
				return ErrorBitmap;
			}
		}
		
		public string Filename { get; }
		
		// Whether this texture is in a separate image or in a tile set
		public bool IsTile { get; }
		
		// Tile number (right-bottom)
		private int Tile = -1;
		
		// Size of one tile
		private int TileSize = -1;
		
		// Number of tiles in one row in the image
		private int TilesRowNum = -1;

		public Texture(string file)
		{
			Filename = file;
			IsTile = false;
		}
		
		public Texture(string file, int tileNumber, int tileSize = 64)
		{
			Filename = file;
			IsTile = true;
			Tile = tileNumber;
			TileSize = tileSize;
			TilesRowNum = (int) ImageLoader.LoadBitmap(Filename).Width / TileSize;
		}

		public int GetX()
		{
			if (IsTile)
			{
				return (Tile % TilesRowNum) * TileSize;
			}

			return 0;
		}
		
		public int GetY()
		{
			if (IsTile)
			{
				return Tile / TilesRowNum * TileSize;
			}

			return 0;
		}
		
		public int GetWidth()
		{
			int width = (int) ImageLoader.LoadBitmap(Filename).Width;
			return IsTile ? width / TilesRowNum : width;
		}

		public int GetHeight()
		{
			// all tiles are square
			return IsTile ? GetWidth() : (int) ImageLoader.LoadBitmap(Filename).Height;
		}

		public D2DBitmap GetBitmap()
		{
			return IsTile ? 
				ImageLoader.LoadBitmapRegion(Filename, GetX(), GetY(), GetWidth(), GetHeight()) :
				ImageLoader.LoadBitmap(Filename);
		}
	}
}