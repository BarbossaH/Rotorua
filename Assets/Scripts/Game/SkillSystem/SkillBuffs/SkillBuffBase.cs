namespace Game.SkillSystem
{
    public abstract class SkillBuffBase
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public float Duration { get; private set; }
        //when buff being added
        public abstract void OnApply();
        //when buff expiring or being dispelled
        public abstract void OnRemove();
    }
}