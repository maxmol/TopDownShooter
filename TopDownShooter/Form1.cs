using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TopDownShooter.Levels;
using MainMenu = System.Windows.Forms.MainMenu;

namespace TopDownShooter
{
	// The game uses Windows Forms but any other rendering engine can be used
	// Only Surface, Texture and Input classes need be changed
	public partial class Form1 : Form
	{
		// Singleton instance of our Form is used by Surface
		public static Form1 Instance { get; private set; } 
		
		// Windows forms' drawing class
		public Graphics Graphics;

		private Stopwatch Stopwatch;
		private Game Game;
		public Form1()
		{
			Instance = this;
			InitializeComponent();
			Graphics = CreateGraphics();
			
			// increase performance (from https://stackoverflow.com/questions/3841905/fastest-way-to-draw-a-series-of-bitmaps-with-c-sharp)
			Graphics.CompositingMode = CompositingMode.SourceOver;
			Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
			Graphics.CompositingQuality = CompositingQuality.HighSpeed;
			Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			Graphics.SmoothingMode = SmoothingMode.None;
			
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

		// Main game timer's tick event
		private void gameLoop_Tick(object sender, EventArgs e)
		{
			// Time since the game started
			double time = Stopwatch.Elapsed.TotalSeconds;
			
			// Time since last frame
			float deltaTime = (float) (time - Game.Time);
			
			// Send current time to the game
			Game.Time = time;
			
			// Update the world
			Game.Tick(deltaTime);
			
			// Draw the world
			Game.Draw(deltaTime);

			Input.Update();
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