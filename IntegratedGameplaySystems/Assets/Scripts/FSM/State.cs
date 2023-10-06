public abstract class State<T>
{
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}