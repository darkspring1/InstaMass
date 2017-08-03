namespace InstaMass.ActorModel.Events
{
    class PlayerCreated
    {
        public string PlayerName { get; }

        public PlayerCreated(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
