using System;
using System.IO;

namespace SkeletalAnimation {

	public struct KeyFrame {

		public TimeSpan Duration { get; }
		public double Rotation { get; }
		public double Length { get; }
		public double Width { get; }

		public KeyFrame(TimeSpan duration, double rotation, double length, double width) {
			Duration = duration;
			Rotation = rotation;
			Length = length;
			Width = width;
		}

		public static KeyFrame ReadFromFile(BinaryReader br) =>
			new KeyFrame(new TimeSpan(br.ReadInt64()), br.ReadDouble(), br.ReadDouble(), br.ReadDouble());

		public static void WriteToFile(BinaryWriter bw, KeyFrame keyFrame) {
			bw.Write(keyFrame.Duration.Ticks);
			bw.Write(keyFrame.Rotation);
			bw.Write(keyFrame.Length);
			bw.Write(keyFrame.Width);
		}

	}

	public class Animation {

		public string Name { get; }

		public ushort initialKeyFrame;
		readonly KeyFrame[] keyFrames;

		public int CurrentFrameIndex { get; set; }

		public TimeSpan TimeSinceFrameChange { get; private set; }

		public KeyFrame CurrentFrame => keyFrames[CurrentFrameIndex];

		public Animation(string name, KeyFrame[] keyFrames) {
			this.keyFrames = keyFrames;
			Name = name;
		}

		public Animation(string name, KeyFrame[] keyFrames, int currentFrameIndex) {
			CurrentFrameIndex = currentFrameIndex;
			this.keyFrames = keyFrames;
			Name = name;
		}

		internal void Tick(TimeSpan t) {
			if ((TimeSinceFrameChange += t) >= CurrentFrame.Duration)
				CurrentFrameIndex++;
		}

		public static Animation ReadFromFile(BinaryReader br) {
			string name = br.ReadString();

			ushort c = br.ReadUInt16();
			KeyFrame[] kfs = new KeyFrame[c];

			for (ushort u = 0; u < c; u++)
				kfs[u] = KeyFrame.ReadFromFile(br);

			ushort initial = br.ReadUInt16();

			return new Animation(name, kfs, initial);
		}

		public static void WriteToFile(BinaryWriter bw, Animation animation) {
			ushort la = (ushort) animation.keyFrames.Length;
			bw.Write(la);

			foreach (KeyFrame kf in animation.keyFrames)
				KeyFrame.WriteToFile(bw, kf);

			bw.Write(animation.initialKeyFrame);
		}
	}

}
