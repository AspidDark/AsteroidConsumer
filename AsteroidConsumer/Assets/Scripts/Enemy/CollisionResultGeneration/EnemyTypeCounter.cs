public static class EnemyTypeCounter {

    public static EnemyType GetEnemyType(float mass, float solid)
    {
        if (mass> Consts.maxMass * Consts.greatestBodyCheckDecreaser&&solid>Consts.maxSolidValue * Consts.greatestBodyCheckDecreaser)
        {
            return EnemyType.gigantBlackHole;
        }
        if (mass * solid > Consts.maxMass * Consts.maxSolidValue * Consts.blackHoleCheckDecreaser)
        {
            return EnemyType.blackHole;
        }
        if (mass> Consts.maxMass*Consts.quazarSolidValueDecreaser && solid > Consts.maxSolidValue * Consts.blackHoleCheckDecreaser)
        {
            return EnemyType.quazar;
        }
        //solid 500 mass 6000
        if (solid> Consts.maxSolidValue * Consts.quazarSolidValueDecreaser && mass > Consts.maxMass * Consts.hugeStarMassDesreaser)
        {
            return EnemyType.hugeStar;
        }
        if (solid*mass > Consts.maxMass * Consts.maxSolidValue*Consts.starDecreaser)
        {
            return EnemyType.star;
        }
        if (solid > Consts.maxSolidValue * Consts.starDecreaser && mass > Consts.maxMass * Consts.starDecreaser)
        {
            return EnemyType.smallStar;
        }
        if (solid *mass > Consts.maxSolidValue*Consts.maxMass * Consts.planetDecreaser)
        {
            return EnemyType.gigantPlanet;
        }
        //150 1500
        if (solid > Consts.maxSolidValue * Consts.planetDecreaser && mass > Consts.maxMass * Consts.planetDecreaser)
        {
            return EnemyType.planet;
        }
        if (mass > Consts.maxMass * Consts.moonDecreaser && solid > Consts.maxSolidValue * Consts.planetDecreaser)
        {
            return EnemyType.moon;
        }
        //100 1000
        if (mass > Consts.maxMass * Consts.moonDecreaser && solid > Consts.maxSolidValue * Consts.moonDecreaser)
        {
            return EnemyType.satellite;
        }
        //50 500
        if (mass > Consts.maxMass * Consts.cometDecreaser && solid > Consts.maxSolidValue * Consts.cometDecreaser)
        {
            return EnemyType.comet;
        }
        if (mass>Consts.largeAsteroidMinMass&&solid> Consts.largeAsteroidMinSolid)
        {
            return EnemyType.largeAsteroid;
        }
        if (mass > Consts.asteroidMinMass)
        {
            return EnemyType.asteroid;
        }
        if (mass > Consts.meteorMinMass)
        {
            return EnemyType.meteor;
        }

        if (mass > Consts.bolidMinMass)
        {
            return EnemyType.bolid;
        }
        return EnemyType.spaceTrash;
    }

}
