using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotController : BaseController, IExecute, IInitialization
{
    #region Fields

    private readonly int _countBot = 5;
    private readonly HashSet<Bot> _botList = new HashSet<Bot>();

    #endregion


    #region Methods

    public void Initialization()
    {
        for (var index = 0; index < _countBot; index++)
        {
            var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<Reference>().Bot,
                Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform,
                Quaternion.identity));

            tempBot.Agent.avoidancePriority = index;
            tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            //todo разных протинвиков
            AddBotToList(tempBot);
        }
    }

    public void AddBotToList(Bot bot)
    {
        if (!_botList.Contains(bot))
        {
            _botList.Add(bot);
            bot.OnDieChange += RemoveBotToList;
        }
    }

    private void RemoveBotToList(Bot bot)
    {
        if (!_botList.Contains(bot))
        {
            return;
        }

        bot.OnDieChange -= RemoveBotToList;
    }

    #endregion

}
