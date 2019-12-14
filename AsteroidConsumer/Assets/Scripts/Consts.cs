using System;

public static class Consts  
{
    //mass
    public const float minMass = 0.01f;
    public const float maxMass = 10000;

    //speed options
    public const float minSpeed=0;
    public const float maxSpeed = 10;
    public const float minChangeDirectionSpeed = 0;
    public const float maxChangeDirectionSpeed = 20;

    //Gravity
    public const float minGravityValue = 0;
    public const float maxGravityValue = 10;

    //Consume
    public const float minConsumePercentage=0;
    public const float maxConsumePercentage = 100;

    //SolidValue
    public const float minSolidValue=-1000;
    public const float maxSolidValue = 1000;



    public const float fullConsumeCompressionMulipluer = 1.25f;
    public const float consumeAndDestroyCompressionMulipluer = 1.2f;
    public const float destroyAndBitConsumeCompressionMulipluer = 1.1f;

    #region Player consts
    public const float playerFullConsumeSolidMultyplyer = 1.1f;
    public const float playerFullConsumeMassMultyplyer = 0.8f;

    public const float playerPartialConsumeSolidMultyplyer = 1.05f;
    public const float playerPartialConsumeMassMultyplyer = 0.6f;
    #endregion




    public const float minMagnitudeValueToInteract = 0.3f;

    public const float minMagnitudeValueToInteractIfStruggle = 0.6f;




    #region Solid
    public const float muchHigherSolid = 3f;
    public const float hiegherSolid = 2f;
    #endregion


    #region EnemyTypeCounter
    public const float greatestBodyCheckDecreaser = 0.9f;
    public const float blackHoleCheckDecreaser = 0.8f;
    public const float quazarSolidValueDecreaser = 0.5f;
    public const float hugeStarMassDesreaser = 0.6f;
    public const float starDecreaser = 0.25f;
    public const float planetDecreaser = 0.15f;
    public const float moonDecreaser = 0.1f;
    public const float satelliteDecreaser = 0.07f;
    public const float cometDecreaser = 0.05f;
    //values
    public const float largeAsteroidMinMass = 400;
    public const float largeAsteroidMinSolid = 45;

    public const float asteroidMinMass = 300;
    public const float meteorMinMass = 200f;
    public const float bolidMinMass = 100f;
    #endregion


    public static readonly float[] mapBodyTypeToMass = { 0, bolidMinMass, meteorMinMass, asteroidMinMass, largeAsteroidMinMass,
    maxMass * cometDecreaser, maxMass * satelliteDecreaser, maxMass *moonDecreaser, maxMass * planetDecreaser, maxMass *(planetDecreaser+starDecreaser)*0.5f,
    maxMass *(planetDecreaser+starDecreaser)*0.75f,  maxMass *starDecreaser,  maxMass * hugeStarMassDesreaser,
    maxMass *(blackHoleCheckDecreaser+hugeStarMassDesreaser)*0.5f, maxMass *blackHoleCheckDecreaser,   maxMass *greatestBodyCheckDecreaser};
}

public enum SpaceBodyType
{
    spaceTrash,
    bolid,
    meteor,
    asteroid,
    largeAsteroid,
    comet,
    satellite,
    moon,
    planet,
    gigantPlanet,
    smallStar,
    star,
    hugeStar,
    quazar,
    blackHole,
    gigantBlackHole
}
public enum ConsumeType
{

    fullyConsumable,
    cousumedAndSpited,
    spited,
    desroyed
}


public enum InitiatorCollisionResult
{
    otherDestroyed,
    noAction,
    bouthDestroyed,
    initiatorDestroyed
}


public enum EnemyGenerationType
{
    randomEvery05Sec,
    randomEvery1Sec,
    randomEvery2Sec,
    ifPlayerIsMoveingEvery05Sec,
    ifPlayerIsMoveingEvery1Sec,
    ifPlayerIsMoveingEvery2Sec,
}


