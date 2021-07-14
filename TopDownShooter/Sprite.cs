using System.Drawing;

namespace TopDownShooter
{
	// Sprite is an entity with a texture
	public class Sprite : Entity
	{
		
		// Texture is a string path to an image
		public Texture Texture { get; set; }

		public float Rotation = 0;

		// Draw an image in the center of sprite's position on screen
		public override void Draw(Surface surface, Camera camera, float deltaTime)
		{
			// calculate screen position
			Vector screenPos = Pos.ToScreen(camera);

			// don't draw anything if the sprite is not on the screen
			if (!surface.IsVisible(screenPos))
				return;
			
			float x = screenPos.X, y = screenPos.Y;

			// scale the texture according to camera zoom
			float w = Texture.GetWidth() * camera.Zoom;
			float h = Texture.GetHeight() * camera.Zoom;
			
			// draw the texture in the center
			if (Rotation == 0)
				surface.DrawTexturedRect(Texture, x - w/2, y - h/2, w, h);
			else
				surface.DrawTexturedRectRotated(Texture, x, y, w, h, Rotation);
		}
	}
}