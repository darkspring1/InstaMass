namespace InstaMass.ActorModel.Events
{
    class PlayerHit
    {
        public int DamageTaken { get; }

        public PlayerHit(int damageTaken)
        {
            DamageTaken = damageTaken;
        }
    }
}
