public class EnemyAbiliteesCounter {

    public EnemyAbiliteesDTO GetEnemyAbilitees(SpaceBodyType enemyType)
    {
        EnemyAbiliteesDTO enemyAbiliteesDTO = new EnemyAbiliteesDTO
        {
            gravityRange=0,
            gravityValue=0,
            hasGavity=false
        };
        switch (enemyType)
        {
            case SpaceBodyType.planet:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 1;
                break;
            case SpaceBodyType.gigantPlanet:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 2;
                break;
            case SpaceBodyType.smallStar:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 3;
                break;
            case SpaceBodyType.star:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 4;
                break;
            case SpaceBodyType.hugeStar:
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 5;
                break;
            case SpaceBodyType.quazar://6
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 6;
                break;
            case SpaceBodyType.blackHole://8
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 8;
                break;
            case SpaceBodyType.gigantBlackHole:  //10
                enemyAbiliteesDTO.hasGavity = true;
                enemyAbiliteesDTO.gravityValue = 10;
                break;
        }
        enemyAbiliteesDTO.gravityRange = enemyAbiliteesDTO.gravityValue * enemyAbiliteesDTO.gravityValue;
        return enemyAbiliteesDTO;
    }
}
