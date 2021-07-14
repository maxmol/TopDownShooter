using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TopDownShooter
{
	public class Texture
	{
		private static class ImageLoader
		{
			private static Dictionary<string, Bitmap> Cache = new();
			private static Dictionary<string, Bitmap> CacheRegions = new();
			private static string ImagesFolder = "Assets/Sprites/";
			private static readonly Bitmap ErrorBitmap = LoadBitmap("error.png");

			public static Bitmap LoadBitmap(string path)
			{
				if (Cache.TryGetValue(path, out Bitmap cachedBitmap))
					return cachedBitmap;

				try
				{
					string fullPath = ImagesFolder + path;
					Bitmap bmp = new Bitmap(fullPath);
					Log.Info("Loaded " + fullPath);
					Cache[path] = bmp;
					return bmp;
				}
				catch (Exception e)
				{
					Log.Error("Error loading material " + path);
					Log.Warning(e.Message);
				}

				return ErrorBitmap;
			}

			public static Bitmap LoadBitmapRegion(string path, int x, int y, int w, int h)
			{
				string cacheKey = $"{path}[{x},{y},{w},{h}]";
				if (CacheRegions.TryGetValue(cacheKey, out Bitmap cachedBitmap))
					return cachedBitmap;

				Rectangle cut = new(x, y, w, h);
				return LoadBitmap(path).Clone(cut, PixelFormat.DontCare);
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
			TilesRowNum = ImageLoader.LoadBitmap(Filename).Width / TileSize;
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
			int width = ImageLoader.LoadBitmap(Filename).Width;
			return IsTile ? width / TilesRowNum : width;
		}

		public int GetHeight()
		{
			// all tiles are square
			return IsTile ? GetWidth() : ImageLoader.LoadBitmap(Filename).Height;
		}

		public Bitmap GetBitmap()
		{
			return IsTile ? 
				ImageLoader.LoadBitmapRegion(Filename, GetX(), GetY(), GetWidth(), GetHeight()) :
				ImageLoader.LoadBitmap(Filename);
		}
	}
}