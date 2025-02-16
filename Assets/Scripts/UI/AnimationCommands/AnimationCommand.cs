using System;
using DG.Tweening;
using UnityEngine;

public abstract class AnimationCommand : ICommand
{
    protected Action _callback;
    protected Sequence s;

    protected abstract void ExecuteCmd();
    public void SetCallback(Action callback)
    {
        _callback = callback;
        Debug.Log($"Callback set of {GetType().Name}");
    }
    
    public virtual void Execute()
    {
        s = DOTween.Sequence();
        s.OnComplete(() => _callback?.Invoke());
        Debug.Log($"Executung {GetType().Name}");
        ExecuteCmd();
        s.Play();
    }

    public virtual void Undo()
    {
    }
}