using System.Collections.Generic;
using System.Drawing;
using TopDownShooter.Entities;

namespace TopDownShooter.Utility
{
	public class Trace
	{
		public struct TraceResult
		{
			public bool Hit;
			public Entity Entity;
			
		}
		
		public Vector StartPos;
		public Vector EndPos;
		public HashSet<Entity> Ignore;
		private Game Game;

		public Trace(Game game, Vector startPos, Vector endPos)
		{
			Game = game;
			StartPos = startPos;
			EndPos = endPos;
			Ignore = new HashSet<Entity>();
		}

		public TraceResult Run()
		{
			TraceResult result = new TraceResult
			{
				Hit = false, 
				Entity = null
			};

			float x1 = StartPos.X;
			float y1 = StartPos.Y;
			float x2 = EndPos.X;
			float y2 = EndPos.Y;

			foreach (IHasCollisionRect collidable in Game.FindEntities<IHasCollisionRect>())
			{
				if (!Ignore.Contains((Entity) collidable))
				{
					Rectangle rect = collidable.GetRectangle();
					if (LineIntersectsRect(x1, y1, x2, y2, rect.X, rect.Y, rect.Width, rect.Height))
					{
						result.Hit = true;
						result.Entity = (Entity) collidable;
						break;
					}
				}
			}

			return result;
		}

		// Algorithms for lines / line and rectangle intersection
		// http://www.jeffreythompson.org/collision-detection/line-rect.php
		
		// line points : x1, y1, x2, y2
		// rectangle position : rx, ry
		// rectangle size : rw, rh
		private bool LineIntersectsRect(float x1, float y1, float x2, float y2, float rx, float ry, float rw, float rh)
		{
			// check if the line has hit any of the rectangle's sides
			// uses the Line/Line function below
			if (LinesIntersect(x1,y1,x2,y2, rx,ry,rx, ry+rh) ||
			    LinesIntersect(x1,y1,x2,y2, rx+rw,ry, rx+rw,ry+rh) ||
			    LinesIntersect(x1,y1,x2,y2, rx,ry, rx+rw,ry) ||
			    LinesIntersect(x1,y1,x2,y2, rx,ry+rh, rx+rw,ry+rh)) {
				return true;
			}
			
			return false;
		}
		
		// line 1 coords : x1, y1, x2, y2
		// line 2 coords : x3, y3, x4, y4
		private bool LinesIntersect(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4) {

			// calculate the direction of the lines
			float uA = ((x4-x3)*(y1-y3) - (y4-y3)*(x1-x3)) / ((y4-y3)*(x2-x1) - (x4-x3)*(y2-y1));
			float uB = ((x2-x1)*(y1-y3) - (y2-y1)*(x1-x3)) / ((y4-y3)*(x2-x1) - (x4-x3)*(y2-y1));

			// if uA and uB are between 0-1, lines are colliding
			if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1) {
				return true;
			}
			return false;
		}

	}
}