﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkeletalAnimation {

	public partial class Viewer : Form {

		private readonly Skeleton skeleton;

		public bool Running { get; private set; } = true;

		private readonly Timer timer;

		private Graphics g;

		public Viewer() {

			string file = GetFile();

			skeleton = Skeleton.LoadFromFile(file);

			InitializeComponent();
			Text = System.IO.Path.GetFileName(file);

			timer = new Timer {
				Interval = 10
			};

			Color backg = Color.FromArgb(50, 50, 50);

			g = canvas.CreateGraphics();
			timer.Tick += (object sender, EventArgs eargs) => {
				g.ResetTransform();
				g.Clear(backg);

				g.TranslateTransform((canvas.Width - controls.Width) / 2, canvas.Height / 2);
				g.RotateTransform(-90);

				skeleton.Tick(TimeSpan.FromMilliseconds(10));
				skeleton.Render(g);
			};
			timer.Start();

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
