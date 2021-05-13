using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkeletalAnimation {

	public partial class Viewer : Form {

		private readonly Skeleton skeleton;

		public bool Running { get; private set; } = true;

		private readonly Timer updateTimer, renderTimer;

		public Viewer() {

			string file = GetFile();

			skeleton = Skeleton.LoadFromFile(file);

			InitializeComponent();
			Text = System.IO.Path.GetFileName(file);

			updateTimer = new Timer {
				Interval = 10
			};

			Color backg = Color.FromArgb(50, 50, 50);

			canvas.Paint += (object sender, PaintEventArgs a) => {
				Graphics g = a.Graphics;
				g.ResetTransform();
				g.Clear(backg);

				g.TranslateTransform(canvas.Width / 2, canvas.Height / 2);
				g.RotateTransform(-90);

				skeleton.Render(g);
			};

			updateTimer.Tick += (object sender, EventArgs eargs) => skeleton.Tick(TimeSpan.FromMilliseconds(updateTimer.Interval));
			updateTimer.Start();

			renderTimer = new Timer {
				Interval = 16
			};
			renderTimer.Tick += (object sender, EventArgs eargs) => canvas.Refresh();
			renderTimer.Start();

		}

		private string GetFile() {
			using (OpenFileDialog ofd = new OpenFileDialog {
				Title = "Choose file to load",
				DefaultExt = ".ske",
				Multiselect = false,
				Filter = "Skeleton files (*.ske) |*.ske",
			}) {

				DialogResult res = ofd.ShowDialog(this);

				if (res != DialogResult.OK)
					Application.Exit();

				return ofd.FileName;
			}
		}

		private void OnPausePlayClick(object sender, EventArgs e) {
			if (Running)
				updateTimer.Stop();
			else
				updateTimer.Start();

			pauseStartButton.Text = (Running = !Running) ? "⏸️" : "▶️";
		}

		private void SpeedOMeterChanged(object sender, EventArgs e) =>
			updateTimer.Interval = (int) ((double) speedOMeter.Value / speedOMeter.Maximum * 100 + 1);

	}
}
