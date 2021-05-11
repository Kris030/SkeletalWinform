using System;

namespace SkeletalAnimation {
	public static class Utils {

		public static double ToRadians(double angle) => Math.PI / 180 * angle;

		public static (double, double) RotatePoint(double x, double y, double oX, double oY, double angle) {
			double sin = Math.Sin(angle), cos = Math.Cos(angle),
					temp = (x - oX) * cos - (y - oY) * sin + oX;

			y = (x - oX) * sin + (y - oY) * cos + oY;
			x = temp;

			return (x, y);
		}

	}
}
