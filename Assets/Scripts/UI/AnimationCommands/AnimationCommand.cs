using System;

public abstract class AnimationCommand : ICommand
{
    protected Action _callback;
    
    public void SetCallback(Action callback)
    {
        _callback = callback;
    }

    public virtual void Execute()
    {
        _callback?.Invoke();
    }

    public virtual void Undo()
    {
    }
}