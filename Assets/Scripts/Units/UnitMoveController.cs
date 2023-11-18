using System;
using RFW;
using RFW.Events;
using UnityEngine;

public class UnitMoveController : IMoveController, ITickable
{
    private IInput _input = null;
    private UnitEvents _unitEvents = null;
    private IUnitView _unitView = null;
    private UnitConfig _unitConfig;

    public bool CanMove
    {
        get;
        set;
    }

    public Type SystemType => typeof(IMoveController);
    public void Init(params object[] parameters)
    {
        _unitEvents = parameters.Get<UnitEvents>();

        _input = parameters.Get<IInput>();
        _input.OnAction -= OnInputAction;
    }

    public void Tick(float deltaTime)
    {
        if (!CanMove)
            return;
    }

    public void Dispose()
    {
        _input.OnAction -= OnInputAction;
        _input = null;
    }

    private void OnInputAction(ActionType action, bool state)
    {

    }
}