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
        IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());

        ServiceLocator.SetService(new TimeRemainingController());
        ServiceLocator.SetService(new Inventory());
        ServiceLocator.SetService(new PlayerController(motor));
        ServiceLocator.SetService(new FlashlightController());
        ServiceLocator.SetService(new WeaponController());
        ServiceLocator.SetService(new InputController());
        ServiceLocator.SetService(new SelectionController());
        ServiceLocator.SetService(new BotController());
        ServiceLocator.SetService(new SaveDataRepository());
        ServiceLocator.SetService(new PhotoController());
        ServiceLocator.SetService(new PauseController());

        _executeControllers = new IExecute[6];

        _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

        _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();

        _executeControllers[2] = ServiceLocator.Resolve<FlashlightController>();

        _executeControllers[3] = ServiceLocator.Resolve<InputController>();

        _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();

        _executeControllers[5] = ServiceLocator.Resolve<BotController>();
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

        ServiceLocator.Resolve<PauseController>().Initialization();
        ServiceLocator.Resolve<Inventory>().Initialization();
        ServiceLocator.Resolve<SelectionController>().On();
        ServiceLocator.Resolve<PlayerController>().On();
        ServiceLocator.Resolve<InputController>().On();
        ServiceLocator.Resolve<BotController>().On();
    }

    private void Cleanup()
    {
        ServiceLocator.Cleanup();
        ServiceLocatorMonoBehaviour.Cleanup();
    }

    #endregion
}
