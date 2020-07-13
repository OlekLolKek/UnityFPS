public class PlayerController : BaseController, IExecute, IInitialization
{
    #region Fields

    private readonly IMotor _motor;

    #endregion


    #region ClassLifeCycles

    public PlayerController(IMotor motor)
    {
        _motor = motor;
    }

    #endregion


    #region Methods

    public void Initialization()
    {
        //UIInterface.HealthBarUI.SetActive(true);
        //UIInterface.HealthTextUi.SetActive(true);
    }

    public void Execute()
    {
        if(!IsActive)
        {
            return;
        }
        _motor.Move();
    }

    #endregion
}
