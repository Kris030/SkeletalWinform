
namespace SkeletalAnimation {
	partial class Viewer {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
			this.canvas = new System.Windows.Forms.PictureBox();
			this.controls = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.controls.SuspendLayout();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.canvas.Dock = System.Windows.Forms.DockStyle.Left;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(604, 450);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = false;
			// 
			// controls
			// 
			this.controls.Controls.Add(this.label1);
			this.controls.Dock = System.Windows.Forms.DockStyle.Right;
			this.controls.Location = new System.Drawing.Point(600, 0);
			this.controls.Name = "controls";
			this.controls.Size = new System.Drawing.Size(200, 450);
			this.controls.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label";
			// 
			// Viewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.controls);
			this.Controls.Add(this.canvas);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Viewer";
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.controls.ResumeLayout(false);
			this.controls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Panel controls;
		private System.Windows.Forms.Label label1;
	}
}

