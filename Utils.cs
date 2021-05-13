using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SkeletalAnimation {
	public static class Utils {

		public static double ToRadians(double degrees) => Math.PI / 180 * degrees;
		public static double ToDegrees(double radians) => 180 / Math.PI * radians;

		public static void WriteToStream(this Image img, BinaryWriter bw, ImageFormat format) {
			MemoryStream ms = new MemoryStream();
			img.Save(ms, format);
			bw.Write((int) ms.Length);
			bw.Write(ms.GetBuffer(), 0, (int) ms.Length);
		}
		public static void WriteToStream(this Image img, BinaryWriter bw) => WriteToStream(img, bw, ImageFormat.Png);

		// worst peace of shit I've ever written
		public static Bitmap ReadFromStream(BinaryReader br) {

			string path = Path.Combine(Path.GetTempPath(), "skeLoading_" + Path.GetRandomFileName());

			File.WriteAllBytes(path, br.ReadBytes(br.ReadInt32()));

			Bitmap bmp;
			using (Image img = Image.FromFile(path))
				bmp = new Bitmap(img);

			File.Delete(path);

			return bmp;
		}

		public static int Clamp(int v, int min, int max) {
			if (v >= max)
				return max;
			if (v <= min)
				return min;
			return v;
		}

		public static double Clamp(double v, double min, double max) {
			if (v >= max)
				return max;
			if (v <= min)
				return min;
			return v;
		}

		public static float Lerp(float s, float e, float t) => (1 - t) * s + t * e;

		public static double Lerp(double s, double e, double t) => (1 - t) * s + t * e;

		public static float Map(float s1, float e1, float s2, float e2, float v) =>
			s2 + (e2 - s2) * ((v - s1) / (e1 - s1));

		public static double Map(double s1, double e1, double s2, double e2, double v) =>
			s2 + (e2 - s2) * ((v - s1) / (e1 - s1));

		public static int AddOneWrap(int val, int start, int end, bool wrap = true) =>
			++val >= end ?
				(wrap ? start : val - 1) :
				val;

	}

}
