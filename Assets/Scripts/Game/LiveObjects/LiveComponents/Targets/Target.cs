namespace SpaceInvaders.LiveObjects.LiveComponents.Targets
{
    public class Target : LiveComponent
    {
        public uint TeamIndex { get; private set; }

        public Target(TargetConstructData data)
        {
            TeamIndex = data.TeamIndex;
        }

        public override void TryReplace(LiveComponent component)
        {
            if (component is not Target target)
                return;

            TeamIndex = target.TeamIndex;
        }
    }
}
