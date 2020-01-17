public static class PlayerInteractionCounter
{
    public static PlayerInteractionEffect GerInteractionResult(EnemyBaseEngine enemyBaseEngine)
    {
         if (enemyBaseEngine == null)
        {
            return PlayerInteractionEffect.noEffect;
        }
        if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / Consts.playerStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        {
            return PlayerInteractionEffect.fullConsume;
        }
        if (enemyBaseEngine.stats.mass * enemyBaseEngine.stats.solidValue / Consts.playerAlsoStrongMutipluer < PlayerStats.instance.Mass * PlayerStats.instance.SolidValue)
        {
            return PlayerInteractionEffect.partialConsume;
        }
        if (enemyBaseEngine.stats.mass / Consts.massiveButNonSolidMass < PlayerStats.instance.Mass
            && enemyBaseEngine.stats.solidValue* Consts.massiveButNonSolidSloidValue < PlayerStats.instance.SolidValue)
        {
            return PlayerInteractionEffect.partialConsume;
        }
        return PlayerInteractionEffect.itConsumesPlayer;
    }
}
