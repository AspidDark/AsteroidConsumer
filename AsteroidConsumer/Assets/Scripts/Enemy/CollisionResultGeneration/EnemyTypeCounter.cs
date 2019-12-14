public static class EnemyTypeCounter {

    public static SpaceBodyType GetEnemyType(float mass, float solid)
    {
        if (mass> Consts.maxMass * Consts.greatestBodyCheckDecreaser&&solid>Consts.maxSolidValue * Consts.greatestBodyCheckDecreaser)
        {
            return SpaceBodyType.gigantBlackHole;
        }
        if (mass * solid > Consts.maxMass * Consts.maxSolidValue * Consts.blackHoleCheckDecreaser)
        {
            return SpaceBodyType.blackHole;
        }
        if (mass> Consts.maxMass*Consts.quazarSolidValueDecreaser && solid > Consts.maxSolidValue * Consts.blackHoleCheckDecreaser)
        {
            return SpaceBodyType.quazar;
        }
        //solid 500 mass 6000
        if (solid> Consts.maxSolidValue * Consts.quazarSolidValueDecreaser && mass > Consts.maxMass * Consts.hugeStarMassDesreaser)
        {
            return SpaceBodyType.hugeStar;
        }
        if (solid*mass > Consts.maxMass * Consts.maxSolidValue*Consts.starDecreaser)
        {
            return SpaceBodyType.star;
        }
        if (solid > Consts.maxSolidValue * Consts.starDecreaser && mass > Consts.maxMass * Consts.starDecreaser)
        {
            return SpaceBodyType.smallStar;
        }
        if (solid *mass > Consts.maxSolidValue*Consts.maxMass * Consts.planetDecreaser)
        {
            return SpaceBodyType.gigantPlanet;
        }
        //150 1500
        if (solid > Consts.maxSolidValue * Consts.planetDecreaser && mass > Consts.maxMass * Consts.planetDecreaser)
        {
            return SpaceBodyType.planet;
        }
        if (mass > Consts.maxMass * Consts.moonDecreaser && solid > Consts.maxSolidValue * Consts.planetDecreaser)
        {
            return SpaceBodyType.moon;
        }
        //100 1000
        if (mass > Consts.maxMass * Consts.satelliteDecreaser && solid > Consts.maxSolidValue * Consts.satelliteDecreaser)
        {
            return SpaceBodyType.satellite;
        }
        //50 500
        if (mass > Consts.maxMass * Consts.cometDecreaser && solid > Consts.maxSolidValue * Consts.cometDecreaser)
        {
            return SpaceBodyType.comet;
        }
        if (mass>Consts.largeAsteroidMinMass&&solid> Consts.largeAsteroidMinSolid)
        {
            return SpaceBodyType.largeAsteroid;
        }
        if (mass > Consts.asteroidMinMass)
        {
            return SpaceBodyType.asteroid;
        }
        if (mass > Consts.meteorMinMass)
        {
            return SpaceBodyType.meteor;
        }

        if (mass > Consts.bolidMinMass)
        {
            return SpaceBodyType.bolid;
        }
        return SpaceBodyType.spaceTrash;
    }

}
