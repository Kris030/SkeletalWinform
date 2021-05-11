using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace SkeletalAnimation {
	public class Bone {

		private readonly Dictionary<string, Animation> animations;
		public Animation CurrentAnimation { get; private set; }

		public void SetAnimation(string name) {
			CurrentAnimation = animations[name];
			foreach (Bone b in Children)
				b.SetAnimation(name);
		}

		public double EndX(double start, double rot) => start + Math.Cos(rot) * CurrentAnimation.CurrentFrame.Length;
		public double EndY(double start, double rot) => start + Math.Sin(rot) * CurrentAnimation.CurrentFrame.Width / 2;

		public ushort? spriteIndex;

		public double SpriteOffsetX { get; }
		public double SpriteOffsetY { get; }
		public Image Sprite { get; }

		public Bone[] Children { get; }

		public Bone(Image sprite, IEnumerable<Animation> animations) {
			Children = new Bone[] { };
			Sprite = sprite;

			this.animations = animations.ToDictionary(a => a.Name);
		}

		public Bone(Image sprite, double spriteOffsetX, double spriteOffsetY, IEnumerable<Animation> animations,
					IEnumerable<Bone> children) : this(sprite, animations) {
			Children = children.ToArray();
			SpriteOffsetX = spriteOffsetX;
			SpriteOffsetY = spriteOffsetY;
		}

		internal void Tick(TimeSpan t) {
			CurrentAnimation.Tick(t);
		}

		internal void Render(Graphics g, double x, double y, double r = 0) {

			double cfr = CurrentAnimation.CurrentFrame.Rotation;

			Matrix original = g.Transform;
			g.RotateTransform((float) cfr);

			if (Sprite != null)
				g.DrawImage(Sprite, (float) (x + SpriteOffsetX), (float) (y + SpriteOffsetY));

			double eX = EndX(x, r + cfr),
				   eY = EndY(y, r + cfr);
			foreach (Bone b in Children)
				b.Render(g, eX, eY, r + cfr);

			g.Transform = original;
		}

		public static Bone ReadFromFile(BinaryReader br, Image[] sprites) {
			Image sprite = br.ReadBoolean() ? sprites[br.ReadUInt16()] : null;

			double sprOffX = br.ReadDouble(),
				   sprOffY = br.ReadDouble();

			ushort animC = br.ReadUInt16();
			Animation[] anims = new Animation[animC];
			for (ushort u = 0; u < animC; u++)
				anims[u] = Animation.ReadFromFile(br);

			ushort chsC = br.ReadUInt16();
			Bone[] chs = new Bone[chsC];
			for (ushort i = 0; i < chsC; i++)
				chs[i] = ReadFromFile(br, sprites);

			return new Bone(sprite, sprOffX, sprOffY, anims, chs);
		}

		public static void WriteToFile(BinaryWriter bw, Bone bone) {
			if (bone.Sprite == null)
				bw.Write(false);
			else {
				bw.Write(true);

				if (bone.spriteIndex.HasValue)
					bw.Write(bone.spriteIndex.Value);
				else
					throw new NullReferenceException(nameof(Bone) + "." + nameof(spriteIndex) + " doesn't have a value.");
			}

			bw.Write(bone.SpriteOffsetX);
			bw.Write(bone.SpriteOffsetY);

			bw.Write((ushort) bone.animations.Count);
			foreach (Animation a in bone.animations.Values)
				Animation.WriteToFile(bw, a);

			bw.Write((ushort) bone.Children.Length);
			foreach (Bone b in bone.Children)
				WriteToFile(bw, b);

		}

		public static readonly Color DEFAULT_COLOR = Color.Yellow;
	}

}
