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

// The proper way would be to add 'additional include directories' and 'additional libs' in Visual Studio Project properties
// Or, you can just be lazy and change these paths for your own system. 
// You must compile ClassiCube in both x86 and x64 configurations to generate the .lib file.
#include "Constants.h"
#include "GameStructs.h"
#include "Chat.h"
#include "Model.h"
#include "Graphics.h"
#include "Entity.h"
#include "ExtMath.h"
#include "Server.h"

// Set the path to the .lib file here or in project settings.
#ifdef _WIN64
//#pragma comment(lib, "x64/Release/ClassiCube.lib")
#else
//#pragma comment(lib, "x86/Release/ClassiCube.lib")
#endif

// use these to cut down on verbose code
#define _SetSize(x,y,z) e->Size = (Vector3) { (x)/16.0f, (y)/16.0f, (z)/16.0f };
#define _SetBounds(x1,y1,z1, x2,y2,z2) e->ModelAABB = (struct AABB) { (x1)/16.0f,(y1)/16.0f,(z1)/16.0f, (x2)/16.0f,(y2)/16.0f,(z2)/16.0f };

// define these as extern, i.e. their actual definition/value is elsewhere (in MoreModels.c)
extern struct ModelTex 
	cape_tex, cape2011_tex, cape2012_tex, cape2013_tex, cape2015_tex, cape2016_tex,
	car_tex, caveSpider_tex, cow_tex, croc_tex,
	enderman_tex, endermanEyes_tex,
	husk_tex, printer_tex,
	magmaCube_tex, male_tex,
	slime_tex, stray_tex,
	tv_tex, villager_tex,
	witherSkeleton_tex, wood_tex,
	zombiePigman_tex, zombieVillager_tex;

// define models
struct Model* CapeModel_GetInstance(void);
struct Model* Cape2011Model_GetInstance(void);
struct Model* Cape2012Model_GetInstance(void);
struct Model* Cape2013Model_GetInstance(void);
struct Model* Cape2015Model_GetInstance(void);
struct Model* Cape2016Model_GetInstance(void);
struct Model* CarModel_GetInstance(void);
struct Model* CaveSpiderModel_GetInstance(void);
struct Model* ChairModel_GetInstance(void);
struct Model* ChibiSitModel_GetInstance(void);
struct Model* CowModel_GetInstance(void);
struct Model* HuskModel_GetInstance(void);
struct Model* MagmaCubeModel_GetInstance(void);
struct Model* SlimeModel_GetInstance(void);
struct Model* StrayModel_GetInstance(void);
struct Model* TModel_GetInstance(void);
struct Model* TableModel_GetInstance(void);
struct Model* TVModel_GetInstance(void);
struct Model* VillagerModel_GetInstance(void);
struct Model* WitherSkeletonModel_GetInstance(void);
struct Model* ZombiePigmanModel_GetInstance(void);
struct Model* ZombieVillagerModel_GetInstance(void);