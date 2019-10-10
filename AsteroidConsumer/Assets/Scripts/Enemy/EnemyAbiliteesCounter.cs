public class EnemyAbiliteesCounter {

    public EnemyAbiliteesDTO GetEnemyAbilitees(EnemyType enemyType)
    {
        EnemyAbiliteesDTO enemyAbiliteesDTO = new EnemyAbiliteesDTO
        {
            gravityRange=0,
            gravityValue=0,
            hasGavity=false
        };
        switch (enemyType)
        {
            case EnemyType.planet:
                break;
            case EnemyType.gigantPlanet:
                break;
            case EnemyType.smallStar:
                break;
            case EnemyType.star:
                break;
            case EnemyType.hugeStar:
                break;
            case EnemyType.quazar:
                break;
            case EnemyType.blackHole:
                break;
            case EnemyType.gigantBlackHole:
                break;
        }
        return enemyAbiliteesDTO;
    }
}
