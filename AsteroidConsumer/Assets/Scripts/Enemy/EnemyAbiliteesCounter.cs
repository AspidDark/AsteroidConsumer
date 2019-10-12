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
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 1;
                break;
            case EnemyType.gigantPlanet:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 2;
                break;
            case EnemyType.smallStar:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 3;
                break;
            case EnemyType.star:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 4;
                break;
            case EnemyType.hugeStar:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 5;
                break;
            case EnemyType.quazar://6
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 6;
                break;
            case EnemyType.blackHole://8
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 8;
                break;
            case EnemyType.gigantBlackHole:  //10
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 10;
                break;
        }
        enemyAbiliteesDTO.gravityRange = enemyAbiliteesDTO.gravityValue * enemyAbiliteesDTO.gravityValue;
        return enemyAbiliteesDTO;
    }
}
