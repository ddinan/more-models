#include "Common.h"
/* All models in this file are derived from the zombie model. */

/* HUSK MODEL */

static struct Model husk;
struct Model* HuskModel_GetInstance(void) {
	husk = *Model_Get(&(String)String_FromConst("zombie"));
	husk.Name = "husk";
	husk.defaultTex = &husk_tex;
	return &husk;
}

/* ZOMBIE PIGMAN MODEL */

static struct Model zpigman;
struct Model* ZombiePigmanModel_GetInstance(void) {
	zpigman = *Model_Get(&(String)String_FromConst("zombie"));
	zpigman.Name = "zombiepigman";
	zpigman.defaultTex = &zombiePigman_tex;
	return &zpigman;
}