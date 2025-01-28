namespace BigModeGameJam.Level.Interactables
{
    public class DestroyOnObjective : ObjectiveListener
    {
        protected override void OnFinishAllCustomCode()
        {
            Destroy(gameObject);
        }
    }
}
