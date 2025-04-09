public abstract class BuffBase
{
    protected float Duration;
    //when buff being added
    public abstract void OnApply();
    //when buff expiring or being dispelled
    public abstract void OnRemove();
}