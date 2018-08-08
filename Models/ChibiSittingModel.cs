using System;
using System.Collections.Generic;
using ClassicalSharp;
using ClassicalSharp.Model;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {

	public class ChibiSittingModel : IModel {
		
		public ChibiSittingModel(Game game) : base(game) {
			CalcHumanAnims = true;
			UsesHumanSkin = true;
			MaxScale = 3.0f;
			ShadowScale = 0.5f;
			armX = 3; armY = 6;
		}
		
		const int sitOffset = 5;
		public override void CreateParts() { }
		public override float NameYOffset { get { return 20.2f/16; } }	
		public override float GetEyeY(Entity entity) { return (14 - sitOffset)/16f; }
		
		public override Vector3 CollisionSize {
			get { return new Vector3(4.6f/16f, (20.1f - sitOffset)/16f, 4.6f/16f); }
		}
		
		public override AABB PickingBounds {
			get { return new AABB(-4/16f, 0, -4/16f, 4/16f, (16 - sitOffset)/16f, 4/16f); }
		}
		
		protected override Matrix4 TransformMatrix(Entity p, Vector3 pos) {
			pos.Y -= (sitOffset / 16f) * p.ModelScale.Y;
			return p.TransformMatrix(p.ModelScale, pos);
		}
		
		public override void DrawModel(Entity p) {
			p.anim.leftLegX =  1.5f; p.anim.rightLegX = 1.5f;
			p.anim.leftLegZ = -0.1f; p.anim.rightLegZ = 0.1f;			
			game.ModelCache.Get("chibi").DrawModel(p);
		}
		
		public override void DrawArm(Entity p) {
			game.ModelCache.Get("chibi").DrawArm(p);
		}
	}
}