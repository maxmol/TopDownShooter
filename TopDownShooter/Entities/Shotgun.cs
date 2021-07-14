namespace TopDownShooter.Entities
{
	public class Shotgun : Sprite, ICanBePickedUp
	{
		public Shotgun()
		{
			Texture = new("shotgun.png");
		}
		
		public float PickUpDistance()
		{
			return 32f;
		}

		public void PickedUp(Player player)
		{
			player.Weapon = Player.Weapons.Shotgun;
			player.Ammo = 3;
			Delete();
		}
	}
}