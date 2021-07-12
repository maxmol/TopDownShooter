using System.Collections.Generic;
using System.Windows.Forms;

namespace TopDownShooter
{
	// All buttons used in the game
	public enum Button
	{
		Up,
		Right,
		Down,
		Left,
		Attack
	}
	
	// Game input handling class
	public static class Input
	{
		// Default control scheme for Windows Forms key codes
		public static Dictionary<Keys, Button> Controls = new()
		{
			[Keys.W] = Button.Up,
			[Keys.A] = Button.Left,
			[Keys.S] = Button.Down,
			[Keys.D] = Button.Right,
			[Keys.Enter] = Button.Attack
		};

		// buttons in binary representation
		// ex: Buttons == 2  <=>  Only Button.Right is pressed down
		private static int ButtonsDown = 0;
		private static int ButtonsPressed = 0;

		// Called on a key press event
		public static void Press(Keys key)
		{
			if (!Controls.TryGetValue(key, out Button btn)) return;
			
			// set bit on button's position to 1
			ButtonsDown |= 1 << (int) btn;
			ButtonsPressed |= 1 << (int) btn;
		}
		
		// Called on a key release event
		public static void Release(Keys key)
		{
			if (!Controls.TryGetValue(key, out Button btn)) return;
			
			// set bit on button's position to 0
			ButtonsDown &= ~(1 << (int) btn);
		}

		// Check if the button is pressed
		public static bool IsDown(Button btn)
		{
			// check if bit on button's position is 1
			return (ButtonsDown & (1 << (int) btn)) != 0;
		}

		// Check if the button was pressed on this tick
		public static bool Pressed(Button btn)
		{
			// check if bit on button's position is 1
			return (ButtonsPressed & (1 << (int) btn)) != 0;
		}

		public static void Update()
		{
			ButtonsPressed = 0;
		}
	}
}