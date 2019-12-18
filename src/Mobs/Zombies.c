#include "Common.h"
/* All models in this file are derived from the zombie model. */

/* HUSK MODEL */

static struct Model husk;
struct Model* HuskModel_GetInstance(void) {
	husk = *Model_Get(&(String)String_FromConst("zombie"));
	husk.name = "husk";
	husk.defaultTex = &husk_tex;
	husk.MakeParts = nullfunc;
	return &husk;
}

/* ZOMBIE PIGMAN MODEL */

static struct Model zpigman;
struct Model* ZombiePigmanModel_GetInstance(void) {
	zpigman = *Model_Get(&(String)String_FromConst("zombie"));
	zpigman.name = "zombiepigman";
	zpigman.defaultTex = &zombiePigman_tex;
	zpigman.MakeParts = nullfunc;
	return &zpigman;
}