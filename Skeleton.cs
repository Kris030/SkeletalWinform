using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace SkeletalAnimation {

	public class Skeleton {

		public Bone Root { get; private set; }
		public double x, y;

		private string currentAnimation;
		public string CurrentAnimation {
			get => currentAnimation;
			set => Root.SetAnimation(currentAnimation = value);
		}

		private Skeleton(string initial, Bone root, double x, double y) {
			this.x = x;
			this.y = y;
			Root = root;
			CurrentAnimation = initial;
		}

		public Skeleton(Bone root, string animation) {
			Root = root;
			CurrentAnimation = animation;
		}

		public Skeleton(Bone root, string animation, double x, double y) : this(root, animation) {
			this.x = x;
			this.y = y;
		}

		/*public Bone GetBoneAtPosition(Point pos) {

			Bone Check(Bone b, double x, double y, double rotation = 0) {
				double r = CurrentAnimation.CurrentFrame.Rotation, a = rotation + r;

				(double rX, double rY) = Utils.RotatePoint(b.EndX(x, a), b.EndY(y, a), x, y, r);

				if (
					pos.X >= rX &&
					pos.Y >= rY &&

					pos.X + CurrentAnimation.CurrentFrame.Length <= rX &&
					pos.Y + CurrentAnimation.CurrentFrame.Width <= rY
					)
					return b;

				foreach (Bone ch in b.children) {
					Bone c = Check(ch, rX, rY, a);
					if (c != null)
						return c;
				}

				return null;
			}

			return Check(Root, x, y);
		}
		*/

		public void Tick(TimeSpan t) {
			Root.Tick(t);

		}

		public void Render(Graphics g, double x = 0, double y = 0) {
			Root.Render(g, x + this.x, y + this.y);


		}

		public static Skeleton LoadFromFile(string file) {
			FileStream fs = new FileStream(file, FileMode.Open);
			using (BinaryReader br = new BinaryReader(fs)) {

				double x = br.ReadDouble(), y = br.ReadDouble();

				ushort ssC = br.ReadUInt16();
				Image[] ss = new Image[ssC];
				for (ushort u = 0; u < ssC; u++)
					ss[u] = Image.FromStream(fs);

				string initialAnimation = br.ReadString();

				return new Skeleton(initialAnimation, Bone.ReadFromFile(br, ss), x, y);
			}
		}

		public static void WriteToFile(string file, Skeleton skeleton, Image[] sprites) {
			using (BinaryWriter bw = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate))) {
				bw.Write(skeleton.x);
				bw.Write(skeleton.y);

				bw.Write((ushort) sprites.Length);
				foreach (Image s in sprites)
					s.Save(bw.BaseStream, ImageFormat.Jpeg);

				bw.Write(skeleton.CurrentAnimation);

				Bone.WriteToFile(bw, skeleton.Root);
			}
		}

	}
}
