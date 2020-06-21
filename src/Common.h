#pragma once
// Since we are building an external plugin dll, we need to import from ClassiCube lib instead of exporting these
#ifdef _WIN32
// need to specifically declare as imported for Visual Studio
#define CC_API __declspec(dllimport)
#define CC_VAR __declspec(dllimport)
#else
#define CC_API
#define CC_VAR
#endif

#include "Block.h"
#include "Entity.h"
#include "ExtMath.h"
#include "Game.h"
#include "Graphics.h"
#include "Model.h"

// use these to cut down on verbose code
#define _SetSize(x,y,z) e->Size = (Vec3) { (x)/16.0f, (y)/16.0f, (z)/16.0f };
#define _SetBounds(x1,y1,z1, x2,y2,z2) e->ModelAABB = (struct AABB) { (x1)/16.0f,(y1)/16.0f,(z1)/16.0f, (x2)/16.0f,(y2)/16.0f,(z2)/16.0f };
#define BOXDESC_REBOUND(desc, X1, Y1, Z1, X2, Y2, Z2) (desc).x1=(X1)/16.f;(desc).y1=(Y1)/16.f;(desc).z1=(Z1)/16.f;(desc).x2=(X2)/16.f;(desc).y2=(Y2)/16.f;(desc).z2=(Z2)/16.f

/* All new textures */
extern struct ModelTex 
	cape_tex, cape2011_tex, cape2012_tex, cape2013_tex, cape2015_tex, cape2016_tex,
	car_tex, carSiren_tex, caveSpider_tex, char_tex, copter_tex, cow_tex, croc_tex,
	enderman_tex, endermanEyes_tex,
	husk_tex, magmaCube_tex, male_tex,
	printer_tex, slime_tex, spiderEyes_tex, stray_tex,
	truck_tex, truckSiren_tex, tv_tex, villager_tex,
	witherSkeleton_tex, wood_tex,
	zombiePigman_tex, zombieVillager_tex;

/* All new models */
struct Model* CapeModel_GetInstance(void);
struct Model* Cape2011Model_GetInstance(void);
struct Model* Cape2012Model_GetInstance(void);
struct Model* Cape2013Model_GetInstance(void);
struct Model* Cape2015Model_GetInstance(void);
struct Model* Cape2016Model_GetInstance(void);
struct Model* CarModel_GetInstance(void);
struct Model* CarSirenModel_GetInstance(void);
struct Model* CaveSpiderModel_GetInstance(void);
struct Model* ChairModel_GetInstance(void);
struct Model* ChibiSitModel_GetInstance(void);
struct Model* CopterModel_GetInstance(void);
struct Model* CowModel_GetInstance(void);
struct Model* CrocModel_GetInstance(void);
struct Model* DabModel_GetInstance(void);
struct Model* EndermanModel_GetInstance(void);
struct Model* FemaleModel_GetInstance(void);
struct Model* FlyModel_GetInstance(void);
struct Model* HeadlessModel_GetInstance(void);
struct Model* HoldModel_GetInstance(void);
struct Model* HuskModel_GetInstance(void);
struct Model* MagmaCubeModel_GetInstance(void);
struct Model* MaleModel_GetInstance(void);
struct Model* PrinterModel_GetInstance(void);
struct Model* SlimeModel_GetInstance(void);
struct Model* StrayModel_GetInstance(void);
struct Model* TModel_GetInstance(void);
struct Model* TableModel_GetInstance(void);
struct Model* TruckModel_GetInstance(void);
struct Model* TruckSirenModel_GetInstance(void);
struct Model* TVModel_GetInstance(void);
struct Model* VillagerModel_GetInstance(void);
struct Model* WitherSkeletonModel_GetInstance(void);
struct Model* ZombiePigmanModel_GetInstance(void);
struct Model* ZombieVillagerModel_GetInstance(void);

// Pointers to existing models
extern struct Model *pig;
void NewPigModel_Init(void);

/* Just your average general-purpose empty function. */
void nullfunc(void);