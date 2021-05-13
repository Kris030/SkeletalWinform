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

			Application.Run(new Viewer());
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
									150, 50
								), new KeyFrame(
									TimeSpan.FromSeconds(2),
									Utils.ToRadians(180),
									100, 20
								), new KeyFrame(
									TimeSpan.FromSeconds(2),
									Utils.ToRadians(90),
									200, 100
								)

							}
						)

					},

					new Bone[] {

						// inner bone 1
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
						},

						// inner bone 2
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
											Utils.ToRadians(-90),
											100, 20
										), new KeyFrame(
											TimeSpan.FromSeconds(1),
											Utils.ToRadians(-180),
											100, 20
										), new KeyFrame(
											TimeSpan.FromSeconds(1),
											Utils.ToRadians(-270),
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

		
	}
}
