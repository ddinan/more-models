using ClassicalSharp;
using ClassicalSharp.Model;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {

	public class ChibiSittingModel : ChibiModel {
		
		public ChibiSittingModel(Game game) : base(game) {
            CalcHumanAnims = false;
			armX = 3; armY = 6;
            ChibiModel chibi = (ChibiModel)game.ModelCache.Get("chibi");
            vertices = chibi.vertices;
            Set = chibi.Set;
            Set64 = chibi.Set64;
            SetSlim = chibi.SetSlim;
        }
		
		const int sitOffset = 5;

		public override float NameYOffset { get { return 20.2f / 16f; } }

		public override float GetEyeY(Entity entity) { return (14 - sitOffset) / 16f; }
		
		public override Vector3 CollisionSize { get { return new Vector3(4.6f / 16f, (20.1f - sitOffset) / 16f, 4.6f / 16f); } }
		
		public override AABB PickingBounds { get { return new AABB(-0.25f, 0, -0.25f, 0.25f, (16 - sitOffset) / 16f, 0.25f); } }
		
		protected override Matrix4 TransformMatrix(Entity p, Vector3 pos) {
			pos.Y -= (sitOffset / 16f) * p.ModelScale.Y;
			return p.TransformMatrix(p.ModelScale, pos);
		}
		
		public override void DrawModel(Entity p) {
			p.anim.leftLegX =  1.5f; p.anim.rightLegX = 1.5f;
			p.anim.leftLegZ = -0.1f; p.anim.rightLegZ = 0.1f;			
			base.DrawModel(p);
		}
		
		public override void DrawArm(Entity p) {
			base.DrawArm(p);
		}
	}
}