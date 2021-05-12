using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkeletalAnimation {
	public partial class Viewer : Form {

		private readonly Skeleton skeleton;

		public bool Running { get; private set; } = true;

		internal static Label debugLabel;

		private readonly Timer timer;

		private Graphics g;

		public Viewer(string file) {

			skeleton = Skeleton.LoadFromFile(file);

			InitializeComponent();
			Text = System.IO.Path.GetFileName(file);

			debugLabel = dLabel;
			BringToFront();

			timer = new Timer {
				Interval = 10
			};

			Color backg = Color.FromArgb(50, 50, 50);

			g = canvas.CreateGraphics();
			timer.Tick += (object sender, EventArgs eargs) => {
				g.ResetTransform();
				g.Clear(backg);

				g.TranslateTransform(canvas.Width / 2, canvas.Height / 2);
				g.RotateTransform(-90);

				skeleton.Render(g);
				skeleton.Tick(TimeSpan.FromMilliseconds(10));

				debugLabel.Text = timer.Interval.ToString();
			};
			timer.Start();

		}

		private void OnPausePlayClick(object sender, EventArgs e) {
			if (Running)
				timer.Stop();
			else
				timer.Start();

			pauseStartButton.Text = (Running = !Running) ? "⏸️" : "▶️";
		}

		private void OnCanvasResize(object sender, EventArgs e) => g = canvas.CreateGraphics();

		private void SpeedOMeterChanged(object sender, EventArgs e) =>
			timer.Interval = (int) ((double) speedOMeter.Value / speedOMeter.Maximum * 100 + 1);

	}
}
