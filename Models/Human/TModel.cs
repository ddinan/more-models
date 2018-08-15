using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;

namespace MoreModels {
	public class TModel : HumanoidModel {
		public TModel(Game game) : base(game) { CalcHumanAnims = false; }

		public override void CreateParts() {
            HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
            vertices = humanoid.vertices;
            Set = humanoid.Set;
            Set64 = humanoid.Set64;
            SetSlim = humanoid.SetSlim;
        }

		public override void DrawModel(Entity p) {
			// No animation for arms/legs
			p.anim.leftArmX = 0f; p.anim.rightArmX = 0f;
			p.anim.leftArmZ = -(float)Math.PI / 2f; p.anim.rightArmZ = (float)Math.PI / 2f;

			p.anim.leftLegX = 0f; p.anim.rightLegX = 0f;
			p.anim.leftLegZ = 0f; p.anim.rightLegZ = 0f;

            base.DrawModel(p);
		}

        public override void DrawArm(Entity p) {
            base.DrawArm(p);
        }
    }
}