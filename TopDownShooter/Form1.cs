using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TopDownShooter.Levels;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;
using MainMenu = System.Windows.Forms.MainMenu;

namespace TopDownShooter
{
	// The game uses Windows Forms but any other rendering engine can be used
	// Only Surface and Texture classes need to be changed
	public partial class Form1 : D2DForm
	{
		// Singleton instance of our Form is used by Surface
		public static Form1 Instance { get; private set; } 
		
		// Windows forms' drawing class
		public D2DGraphics Direct2D;

		private Stopwatch Stopwatch;
		private Game Game;
		public Form1()
		{
			Instance = this;
			InitializeComponent();

			// Add keyboard events
			KeyPreview = true;
			KeyDown += Form1_KeyDown;
			KeyUp += Form1_KeyUp;

			// Start measuring time
			Stopwatch = Stopwatch.StartNew();
			
			// Create a Game instance
			Game = new Game();
			
			// Load a level
			Game.SetLevel<MainMenuLevel>();
		}

		protected override void OnRender(D2DGraphics g)
		{
			base.OnRender(g);

			// Time since the game started
			double time = Stopwatch.Elapsed.TotalSeconds;
			
			// Time since last frame
			float deltaTime = (float) (time - Game.Time);
			
			// Send current time to the game
			Game.Time = time;
			
			// Update the world
			Game.Tick(deltaTime);
			
			Direct2D = g;
			
			// Draw the world
			Game.Draw(deltaTime);
			
			// Refresh the input (clear pressed buttons)
			Input.Update();
			
			Invalidate();
		}

		// Key release event
		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			// Send a `key release` signal to Game's Input
			Input.Release(e.KeyCode);
		}
	
		// Key press event
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			// Send a `key press` signal to the Game's Input
			Input.Press(e.KeyCode);
		}
	}
}