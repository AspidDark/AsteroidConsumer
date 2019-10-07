using System;

public static class Consts  
{
    //mass
    public const float minMass = 0.05f;
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
    public const float minSolidValuePercentage=-100;
    public const float maxSolidValuePercentage = 100;



    public const float fullConsumeCompressionMulipluer = 1.1f; //1.1??


}

public enum EnemyType
{
    spaceTrash,
    meteor,
    asteroid,
    comet,
    largeAsteroid,
    lagreComet,
    satellite,
    moon,
    smallPlanet,
    planet,
    hugePlanet,
    gigantPlanet,
    smallStar,
    star,
    hugeStar,
    quazar,
    BlackHole,
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
