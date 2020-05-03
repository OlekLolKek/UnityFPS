using UnityEngine;


public sealed class Controllers : IInitialization
{
    #region Fields

    public IExecute this[int index] => _executeControllers[index];
    public int Length => _executeControllers.Length;

    private readonly IExecute[] _executeControllers;

    #endregion


    #region ClassLifeCycles

    public Controllers()
    {
        IMotor motor = default;
        if (Application.platform == RuntimePlatform.PS4)
        {
            //
        }
        else
        {
            motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
        }
        ServiceLocator.SetService(new PlayerController(motor));
        ServiceLocator.SetService(new FlashlightController());
        ServiceLocator.SetService(new InputController());
        _executeControllers = new IExecute[3];

        _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();

        _executeControllers[1] = ServiceLocator.Resolve<FlashlightController>();

        _executeControllers[2] = ServiceLocator.Resolve<InputController>();
    }

    #endregion


    #region Methods

    public void Initialization()
    {
        foreach (var controller in _executeControllers)
        {
            if (controller is IInitialization initialization)
            {
                initialization.Initialization();
            }
        }

        ServiceLocator.Resolve<PlayerController>().On();
        ServiceLocator.Resolve<InputController>().On();
    }

    #endregion
}
