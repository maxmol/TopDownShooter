using System.Drawing;

namespace TopDownShooter
{
	// Sprite is an entity with a texture
	public class Sprite : Entity
	{
		
		// Texture is a string path to an image
		public string Texture { get; set; }

		public float Rotation = 0;

		// Draw an image in the center of sprite's position on screen
		public override void Draw(Surface surface, Camera camera, float deltaTime)
		{
			// calculate screen position
			Vector screenPos = Pos.ToScreen(camera);
			float x = screenPos.X, y = screenPos.Y;

			// scale the texture according to camera zoom
			float w = surface.TextureWidth(Texture) * camera.Zoom;
			float h = surface.TextureHeight(Texture) * camera.Zoom;
			
			// draw the texture in the center
			if (Rotation == 0)
				surface.DrawTexturedRect(Texture, x - w/2, y - h/2, w, h);
			else
				surface.DrawTexturedRectRotated(Texture, x, y, w, h, Rotation);
		}
	}
}