using System;
using System.IO;

namespace SkeletalAnimation {

	public struct KeyFrame {

		public TimeSpan Duration { get; }
		public double Rotation { get; }
		public double Length { get; }
		public double Width { get; }

		public KeyFrame(TimeSpan duration, double rotation, double length, double width) {
			if (duration <= TimeSpan.Zero)
				throw new ArgumentException("Duration can't be negative or zero.");

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

		public bool Loop { get; }

		public ushort initialKeyFrame;
		readonly KeyFrame[] keyFrames;

		public KeyFrame CurrentFrame => keyFrames[CurrentFrameIndex];
		public TimeSpan TimeSinceFrameChange { get; private set; }

		public KeyFrame PreviousFrame => keyFrames[PreviousFrameIndex];
		public KeyFrame NextFrame => keyFrames[NextFrameIndex];

		public int CurrentFrameIndex { get; private set; }
		public int PreviousFrameIndex { get; private set; }
		public int NextFrameIndex { get; private set; }

		private void NextKeyFrame() {
			PreviousFrameIndex = CurrentFrameIndex;
			CurrentFrameIndex = Utils.AddOneWrap(PreviousFrameIndex, 0, keyFrames.Length);
			NextFrameIndex = Utils.AddOneWrap(CurrentFrameIndex, 0, keyFrames.Length);
		}

		public Animation(string name, KeyFrame[] keyFrames) {
			this.keyFrames = keyFrames;
			Name = name;
			Loop = true;

			NextFrameIndex = Utils.AddOneWrap(CurrentFrameIndex, 0, keyFrames.Length);
		}

		public Animation(string name, KeyFrame[] keyFrames, int currentFrameIndex, bool loop) {
			this.keyFrames = keyFrames;
			
			PreviousFrameIndex = CurrentFrameIndex = currentFrameIndex;
			NextFrameIndex = Utils.AddOneWrap(currentFrameIndex, 0, keyFrames.Length);

			Name = name;
			Loop = loop;
		}

		internal void Tick(TimeSpan t) {
			TimeSinceFrameChange += t;

			while (TimeSinceFrameChange >= CurrentFrame.Duration) {
				TimeSinceFrameChange -= CurrentFrame.Duration;
				NextKeyFrame();
			}

		}

		public static Animation ReadFromFile(BinaryReader br) {
			string name = br.ReadString();
			bool loop = br.ReadBoolean();

			ushort c = br.ReadUInt16();
			KeyFrame[] kfs = new KeyFrame[c];

			for (ushort u = 0; u < c; u++)
				kfs[u] = KeyFrame.ReadFromFile(br);

			ushort initial = br.ReadUInt16();

			return new Animation(name, kfs, initial, loop);
		}

		public static void WriteToFile(BinaryWriter bw, Animation animation) {
			bw.Write(animation.Name);
			bw.Write(animation.Loop);

			ushort la = (ushort) animation.keyFrames.Length;
			bw.Write(la);

			foreach (KeyFrame kf in animation.keyFrames)
				KeyFrame.WriteToFile(bw, kf);

			bw.Write(animation.initialKeyFrame);
		}
	}

}
