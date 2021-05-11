using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkeletalAnimation {
	public partial class Viewer : Form {

		private readonly Skeleton skeleton;

		internal static Label label;

		public Viewer(string file) {

			skeleton = Skeleton.LoadFromFile(file);

			InitializeComponent();
			Text = System.IO.Path.GetFileName(file);

			label = label1;
			BringToFront();

			Timer t = new Timer {
				Interval = 10
			};
			Graphics g = canvas.CreateGraphics();
			t.Tick += (object sender, EventArgs eargs) => {
				g.ResetTransform();
				//g.FillRectangle(Brushes.Black, 0, 0, canvas.Width, canvas.Height);
				g.TranslateTransform(canvas.Width / 2, canvas.Height / 2);
				g.RotateTransform(-90);
				skeleton.Render(g);
			};
			t.Start();

		}

	}
}
