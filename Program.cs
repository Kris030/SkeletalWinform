using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SkeletalAnimation {
	internal static class Program {

		[STAThread]
		static void Main() {

			CreateTestSkeleton();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string file = GetFile();
			Application.Run(new Viewer(file));
		}

		private static void CreateTestSkeleton() {

			Image sprite1 = Image.FromFile("res/testSprite.png");

			Skeleton s = new Skeleton(

				// first
				new Bone(
					sprite1,
					0, 0,
					new Animation[] {

						new Animation(
							"anim1",
							new KeyFrame[] {

								new KeyFrame(
									TimeSpan.FromSeconds(2),
									0,
									100, 20
								), new KeyFrame(
									TimeSpan.FromSeconds(2),
									Utils.ToRadians(90),
									100, 20
								), new KeyFrame(
									TimeSpan.FromSeconds(2),
									Utils.ToRadians(180),
									100, 20
								), new KeyFrame(
									TimeSpan.FromSeconds(2),
									Utils.ToRadians(90),
									100, 20
								)

							}
						)

					},

					new Bone[] {

						// inner bone
						new Bone(
							sprite1,
							new Animation[] {
								new Animation(
									"anim1",
									new KeyFrame[] {

										new KeyFrame(
											TimeSpan.FromSeconds(1),
											0,
											100, 20
										), new KeyFrame(
											TimeSpan.FromSeconds(1),
											Utils.ToRadians(90),
											100, 20
										), new KeyFrame(
											TimeSpan.FromSeconds(1),
											Utils.ToRadians(180),
											100, 20
										), new KeyFrame(
											TimeSpan.FromSeconds(1),
											Utils.ToRadians(270),
											100, 20
										)

									}
								)
							}
						) {
							spriteIndex = 0
						}
					}
				) {
					spriteIndex = 0
				},
				"anim1"
			);

			Skeleton.WriteToFile("test.ske", s, new Image[] {
				sprite1,
			});
		}

		private static string GetFile() {
			using (OpenFileDialog ofd = new OpenFileDialog {
				Title = "Choose file to load",
				DefaultExt = ".ske",
				Multiselect = false,
				Filter = "Skeleton files (*.ske) |*.ske"
			}) {

				DialogResult res = ofd.ShowDialog();

				if (res != DialogResult.OK)
					Application.Exit();

				return ofd.FileName;
			}
		}
	}
}
