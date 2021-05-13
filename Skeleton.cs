using System;
using System.IO;
using System.Linq;
using System.Drawing;
using SkeletalAnimation;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace SkeletalAnimation {

	public class Skeleton {

		public double X { get; }
		public double Y { get; }
		public double Rotation { get; }

		public Bone Root { get; private set; }

		private string currentAnimation;
		public string CurrentAnimation {
			get => currentAnimation;
			set => Root.SetAnimation(currentAnimation = value);
		}

		private Skeleton(string initial, Bone root, double x, double y, double rotation) {
			X = x;
			Y = y;
			Root = root;
			CurrentAnimation = initial;
		}

		public Skeleton(Bone root, string animation) {
			Root = root;
			CurrentAnimation = animation;
		}

		public Skeleton(Bone root, string animation, double x, double y, double rotation) : this(root, animation) {
			X = x;
			Y = y;
			Rotation = rotation;
		}

		public void Tick(TimeSpan t) {
			Root.Tick(t);
		}

		public void Render(Graphics g) {
			g.TranslateTransform((float) X, (float) Y);
			g.RotateTransform((float) Rotation);
			Root.Render(g, 0);
		}

		public static Skeleton LoadFromFile(string file) {
			using (BinaryReader br = new BinaryReader(new FileStream(file, FileMode.Open))) {

				double x = br.ReadDouble(), y = br.ReadDouble(), rot = br.ReadDouble();

				ushort ssC = br.ReadUInt16();
				Image[] ss = new Image[ssC];
				for (ushort u = 0; u < ssC; u++)
					ss[u] = Utils.ReadFromStream(br);

				string initialAnimation = br.ReadString();

				return new Skeleton(initialAnimation, Bone.ReadFromFile(br, ss), x, y, rot);
			}
		}

		public static void WriteToFile(string file, Skeleton skeleton, Image[] sprites) {
			using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate))) {
				bw.Write(skeleton.X);
				bw.Write(skeleton.Y);
				bw.Write(skeleton.Rotation);

				bw.Write((ushort) sprites.Length);
				foreach (Image s in sprites)
					s.WriteToStream(bw);

				bw.Write(skeleton.CurrentAnimation);

				Bone.WriteToFile(bw, skeleton.Root);
			}
		}

	}
}
