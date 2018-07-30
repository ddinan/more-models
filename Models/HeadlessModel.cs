using System;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model {
	
	public class HeadlessModel : IModel {
		
		public ModelSet Set, SetSlim, Set64;
		public HeadlessModel(Game window) : base(window) {
			CalcHumanAnims = true;
			UsesHumanSkin = true;
		}
		
		protected BoxDesc torso, lLeg, rLeg, lArm, rArm;
		protected float offset = 0.5f;
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * (6 + 6 + 4)];
			Set = new ModelSet();
			MakeDescriptions();
			
			Set.Torso = BuildBox(torso.TexOrigin(16, 16));
			Set.LeftLeg = BuildBox(lLeg.MirrorX().TexOrigin(0, 16));
			Set.RightLeg = BuildBox(rLeg.TexOrigin(0, 16));
			Set.LeftArm = BuildBox(lArm.MirrorX().TexOrigin(40, 16));
			Set.RightArm = BuildBox(rArm.TexOrigin(40, 16));
			lArm = lArm.MirrorX(); lLeg = lLeg.MirrorX();
			
			Set64 = new ModelSet();
			Set64.Torso = Set.Torso;
			Set64.LeftLeg = BuildBox(lLeg.TexOrigin(16, 48));
			Set64.RightLeg = Set.RightLeg;
			Set64.LeftArm = BuildBox(lArm.TexOrigin(32, 48));
			Set64.RightArm = Set.RightArm;
			
			Set64.TorsoLayer = BuildBox(torso.TexOrigin(16, 32).Expand(offset));
			Set64.LeftLegLayer = BuildBox(lLeg.TexOrigin(0, 48).Expand(offset));
			Set64.RightLegLayer = BuildBox(rLeg.TexOrigin(0, 32).Expand(offset));
			Set64.LeftArmLayer = BuildBox(lArm.TexOrigin(48, 48).Expand(offset));
			Set64.RightArmLayer = BuildBox(rArm.TexOrigin(40, 32).Expand(offset));
			
			SetSlim = new ModelSet();
			SetSlim.Torso = Set64.Torso;
			SetSlim.LeftLeg = Set64.LeftLeg;
			SetSlim.RightLeg = Set64.RightLeg;
			lArm.BodyW -= 1; lArm.X1 += (offset * 2)/16f;
			SetSlim.LeftArm = BuildBox(lArm.TexOrigin(32, 48));
			rArm.BodyW -= 1; rArm.X2 -= (offset * 2)/16f;
			SetSlim.RightArm = BuildBox(rArm.TexOrigin(40, 16));
			
			SetSlim.TorsoLayer = Set64.TorsoLayer;
			SetSlim.LeftLegLayer = Set64.LeftLegLayer;
			SetSlim.RightLegLayer = Set64.RightLegLayer;
			SetSlim.LeftArmLayer = BuildBox(lArm.TexOrigin(48, 48).Expand(offset));
			SetSlim.RightArmLayer = BuildBox(rArm.TexOrigin(40, 32).Expand(offset));
		}
		
		protected virtual void MakeDescriptions() {
			torso = MakeBoxBounds(-4, 12, -2, 4, 24, 2);
			lLeg = MakeBoxBounds(-4, 0, -2, 0, 12, 2).RotOrigin(0, 12, 0);
			rLeg = MakeBoxBounds(0, 0, -2, 4, 12, 2).RotOrigin(0, 12, 0);
			lArm = MakeBoxBounds(-8, 12, -2, -4, 24, 2).RotOrigin(-5, 22, 0);
			rArm = MakeBoxBounds(4, 12, -2, 8, 24, 2).RotOrigin(5, 22, 0);
		}
		
		public override float NameYOffset { get { return 32/16f + 0.5f/16f; } }
		
		public override float GetEyeY(Entity entity) { return 26/16f; }
		
		public override Vector3 CollisionSize {
			get { return new Vector3(8/16f + 0.6f/16f, 28.1f/16f, 8/16f + 0.6f/16f); }
		}
		
		public override AABB PickingBounds {
			get { return new AABB(-8/16f, 0, -4/16f, 8/16f, 32/16f, 4/16f); }
		}
		
		public override void DrawModel(Entity p) {
			game.Graphics.BindTexture(GetTexture(p));
			game.Graphics.AlphaTest = false;
			
			bool _64x64 = p.SkinType != SkinType.Type64x32;
			uScale = p.uScale / 64f;
			vScale = p.vScale / (_64x64 ? 64 : 32);
			RenderParts(p);
		}
		
		protected virtual void RenderParts(Entity p) {
			SkinType skinType = p.SkinType;
			ModelSet model = skinType == SkinType.Type64x64Slim ? SetSlim :
				(skinType == SkinType.Type64x64 ? Set64 : Set);
			
			DrawPart(model.Torso);
			
			DrawRotate(p.anim.leftLegX, 0, p.anim.leftLegZ, model.LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0, p.anim.rightLegZ, model.RightLeg, false);
			Rotate = RotateOrder.XZY;
			DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, model.LeftArm, false);
			DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, model.RightArm, false);
			Rotate = RotateOrder.ZYX;
			UpdateVB();
			
			game.Graphics.AlphaTest = true;
			index = 0;
			if (skinType != SkinType.Type64x32) {
				DrawPart(model.TorsoLayer);
				DrawRotate(p.anim.leftLegX, 0, p.anim.leftLegZ, model.LeftLegLayer, false);
				DrawRotate(p.anim.rightLegX, 0, p.anim.rightLegZ, model.RightLegLayer, false);
				Rotate = RotateOrder.XZY;
				DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, model.LeftArmLayer, false);
				DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, model.RightArmLayer, false);
				Rotate = RotateOrder.ZYX;
			}
			UpdateVB();
		}
		
		public class ModelSet {
			public ModelPart Torso, LeftLeg, RightLeg, LeftArm, RightArm, TorsoLayer,
			LeftLegLayer, RightLegLayer, LeftArmLayer, RightArmLayer;
		}
	}
}