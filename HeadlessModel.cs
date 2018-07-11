using System;
namespace ClassicalSharp.Model {
	
	public class HeadlessModel : HumanoidModel {
		public HeadlessModel(Game window) : base(window) { }
		
		protected override void MakeDescriptions() {
			base.MakeDescriptions();
			head = MakeBoxBounds(0, 0, 0, 0, 0, 0).RotOrigin(0, 0, 0);
		}
	}
}
