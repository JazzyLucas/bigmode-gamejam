namespace BigModeGameJam.Level.Manager
{
    public interface IObjectiveListener
    {
        public Objective[] Objectives { get; }

        public void ObjectiveCaller(Objective objective);
    }
}
