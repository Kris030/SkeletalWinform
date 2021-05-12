
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
			this.speedOMeterLabel = new System.Windows.Forms.Label();
			this.speedOMeter = new System.Windows.Forms.TrackBar();
			this.pauseStartButton = new System.Windows.Forms.Button();
			this.dLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.controls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.speedOMeter)).BeginInit();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Left;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(600, 450);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = false;
			// 
			// controls
			// 
			this.controls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.controls.Controls.Add(this.speedOMeterLabel);
			this.controls.Controls.Add(this.speedOMeter);
			this.controls.Controls.Add(this.pauseStartButton);
			this.controls.Controls.Add(this.dLabel);
			this.controls.Location = new System.Drawing.Point(600, 0);
			this.controls.Name = "controls";
			this.controls.Size = new System.Drawing.Size(200, 450);
			this.controls.TabIndex = 1;
			// 
			// speedOMeterLabel
			// 
			this.speedOMeterLabel.AutoSize = true;
			this.speedOMeterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.speedOMeterLabel.Location = new System.Drawing.Point(6, 365);
			this.speedOMeterLabel.Name = "speedOMeterLabel";
			this.speedOMeterLabel.Size = new System.Drawing.Size(108, 25);
			this.speedOMeterLabel.TabIndex = 3;
			this.speedOMeterLabel.Text = "Sebesség";
			// 
			// speedOMeter
			// 
			this.speedOMeter.LargeChange = 2;
			this.speedOMeter.Location = new System.Drawing.Point(6, 393);
			this.speedOMeter.Maximum = 20;
			this.speedOMeter.Name = "speedOMeter";
			this.speedOMeter.Size = new System.Drawing.Size(188, 45);
			this.speedOMeter.TabIndex = 2;
			this.speedOMeter.Value = 1;
			this.speedOMeter.ValueChanged += new System.EventHandler(this.SpeedOMeterChanged);
			// 
			// pauseStartButton
			// 
			this.pauseStartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pauseStartButton.Location = new System.Drawing.Point(113, 12);
			this.pauseStartButton.Name = "pauseStartButton";
			this.pauseStartButton.Size = new System.Drawing.Size(75, 75);
			this.pauseStartButton.TabIndex = 1;
			this.pauseStartButton.Text = "⏸️";
			this.pauseStartButton.UseVisualStyleBackColor = true;
			this.pauseStartButton.Click += new System.EventHandler(this.OnPausePlayClick);
			// 
			// dLabel
			// 
			this.dLabel.AutoSize = true;
			this.dLabel.Location = new System.Drawing.Point(3, 9);
			this.dLabel.Name = "dLabel";
			this.dLabel.Size = new System.Drawing.Size(51, 13);
			this.dLabel.TabIndex = 0;
			this.dLabel.Text = "[DEBUG]";
			// 
			// Viewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.controls);
			this.Controls.Add(this.canvas);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Viewer";
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.controls.ResumeLayout(false);
			this.controls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.speedOMeter)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Panel controls;
		private System.Windows.Forms.Label dLabel;
		private System.Windows.Forms.Button pauseStartButton;
		private System.Windows.Forms.Label speedOMeterLabel;
		private System.Windows.Forms.TrackBar speedOMeter;
	}
}

