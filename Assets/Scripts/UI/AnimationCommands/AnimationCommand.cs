using System;
using DG.Tweening;
using UnityEngine;

public abstract class AnimationCommand : ICommand
{
    protected Action _callback;
    protected Sequence s;

    protected abstract void ExecuteCmd();
    public virtual void SetCallback(Action callback)
    {
        _callback = callback;
    }
    
    public virtual void Execute()
    {
        s = DOTween.Sequence();
        s.OnComplete(() => _callback?.Invoke());
        ExecuteCmd();
        s.Play();
    }

    public virtual void Undo()
    {
    }
}