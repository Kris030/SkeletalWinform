using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace SkeletalAnimation {

	public partial class Viewer : Form {

		private readonly Skeleton skeleton;

		public bool Running { get; private set; } = true;

		private readonly Timer updateTimer, renderTimer;

		public double Zoom { get; private set; } = 1;

		public Viewer() {

			string file = GetFile();

			skeleton = Skeleton.LoadFromFile(file);

			InitializeComponent();
			Text = Path.GetFileName(file);

			updateTimer = new Timer {
				Interval = 10
			};

			Color backg = Color.FromArgb(50, 50, 50);

			canvas.Paint += (object sender, PaintEventArgs a) => {
				Graphics g = a.Graphics;

				g.Clear(backg);

				g.TranslateTransform(canvas.Width / 2, canvas.Height / 2);
				//g.RotateTransform(-90);

				g.TranslateTransform((float) CanvasOffsetX, (float) CanvasOffsetY);
				g.ScaleTransform((float) Zoom, (float) Zoom);

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

			Running = !Running;
			pauseStartButton.Text = Running ? "⏸️" : "⏵";
		}

		private void OnSpeedOMeterChanged(object sender, EventArgs e) =>
			updateTimer.Interval = (int) ((double) speedOMeter.Value / speedOMeter.Maximum * 100 + 1);

		private const double ZOOM_INTENSITY = .01, MIN_ZOOM = .1, MAX_ZOOM = 2;

		private void OnCanvasWheel(object sender, MouseEventArgs e) {
			double deltaZ = e.Delta * ZOOM_INTENSITY;

			Zoom = Utils.Clamp(
				e.Delta > 0 ?
					Zoom * deltaZ :
					Zoom / -deltaZ,

				MIN_ZOOM,
				MAX_ZOOM
			);
		}

		public bool Dragging { get; private set; }
		public Point DraggingOrigin { get; private set; }

		public double CanvasOffsetX { get; private set; }
		public double CanvasOffsetY { get; private set; }

		private void OnCanvasMouseDown(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Dragging = true;
				DraggingOrigin = new Point(
					(int) Math.Round(e.X - CanvasOffsetX),
					(int) Math.Round(e.Y - CanvasOffsetY)
				);
			}
		}

		private void OnCanvasMouseMove(object sender, MouseEventArgs e) {
			if (Dragging) {
				CanvasOffsetX = e.Location.X - DraggingOrigin.X;
				CanvasOffsetY = e.Location.Y - DraggingOrigin.Y;
			}
		}

		private void OnCanvasMouseUp(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Dragging = false;
				DraggingOrigin = default;
			}
		}

	}
}
