#include "Common.h"
/* All models in this file are derived from the skeleton model. */
static struct Model* skeleton;

/* STRAY MODEL */

static struct Model stray;
struct Model* StrayModel_GetInstance(void) {
	// First skeleton-based model initialized gets to set skeleton ptr.
	stray = *(skeleton = Model_Get(&(String)String_FromConst("skeleton")));
	stray.Name = "stray";
	stray.defaultTex = &stray_tex;
	return &stray;
}

/* WITHER SKELETON MODEL */

static void WitherSkeletonModel_GetTransform(struct Entity* e, Vector3 pos, struct Matrix* m) {
	static Vector3 vec;	vec = e->ModelScale;
	Vector3_Mul1(&vec, &vec, 1.25f);
	Entity_GetTransform(e, pos, vec, m);
}
static float WitherSkeletonModel_GetEyeY(struct Entity* e) { return skeleton->GetEyeY(e) * 1.25f; }

static void WitherSkeletonModel_GetSize(struct Entity* e) {
	skeleton->GetCollisionSize(e); Vector3_Mul1(&e->Size, &e->Size, 1.25f);
}
static void WitherSkeletonModel_GetBounds(struct Entity* e) {
	skeleton->GetPickingBounds(e);
	e->ModelAABB.Min.X *= 1.125f; e->ModelAABB.Min.Z *= 1.125f;
	e->ModelAABB.Max.X *= 1.125f; e->ModelAABB.Max.Z *= 1.125f;
	e->ModelAABB.Max.Y *= 1.25f;
}
static struct Model witherskel;
struct Model* WitherSkeletonModel_GetInstance(void) {
	witherskel = *Model_Get(&(String)String_FromConst("skeleton"));
	witherskel.Name = "witherskeleton";
	witherskel.defaultTex = &witherSkeleton_tex;
	witherskel.GetTransform = WitherSkeletonModel_GetTransform;
	witherskel.GetCollisionSize = WitherSkeletonModel_GetSize;
	witherskel.GetEyeY = WitherSkeletonModel_GetEyeY;
	witherskel.GetPickingBounds = WitherSkeletonModel_GetBounds;
	return &witherskel;
}