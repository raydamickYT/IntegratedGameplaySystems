using System;

public class ActorBase
{
    public Action OnUpdateEvent;
    public Action OnFixedUpdateEvent;

    public Action NoLongerMoving;
    public Action StartedMoving;
}
