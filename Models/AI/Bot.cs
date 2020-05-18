using UnityEngine;
using System;
using UnityEngine.AI;


public sealed class Bot : BaseObjectScene, IExecute
{
    #region Fields

    public float HP = 100;
    public Vision Vision;
    public Weapon Weapon;

    private float _waitTime = 3;
    private StateBot _stateBot;
    private Vector3 _point;
    private float _stoppingDistance = 2.0f;

    public event Action<Bot> OnDieChange;
    private ITimeRemaining _timeRemaining;

    #endregion


    #region Properties

    public Transform Target { get; set; }
    public NavMeshAgent Agent { get; private set; }

    private StateBot StateBot
    {
        get => _stateBot;
        set
        {
            _stateBot = value;
            switch (value)
            {
                case StateBot.None:
                    Color = Color.white;
                    break;
                case StateBot.Patrol:
                    Color = Color.green;
                    break;
                case StateBot.Inspection:
                    Color = Color.yellow;
                    break;
                case StateBot.Detected:
                    Color = Color.red;
                    break;
                case StateBot.Dead:
                    Color = Color.gray;
                    break;
                default:
                    Color = Color.white;
                    break; 
            }
        }
    }

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
    }

    private void OnEnable()
    {
        var bodyBot = GetComponentInChildren<BodyBot>();
        if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
    }

    private void OnDisable()
    {
        var bodyBot = GetComponentInChildren<BodyBot>();
        if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
    }

    #endregion


    #region Methods

    public void Execute()
    {
        if (StateBot == StateBot.Dead) return;

        if (StateBot != StateBot.Detected)
        {
            if (!Agent.hasPath)
            {
                if (StateBot != StateBot.Inspection)
                {
                    if (StateBot != StateBot.Patrol)
                    {
                        StateBot = StateBot.Patrol;
                        _point = Patrol.GenericPoint(transform);
                        MovePoint(_point);
                        Agent.stoppingDistance = 0;
                    }
                    else
                    {
                        if ((_point - transform.position).sqrMagnitude <= 1)
                        {
                            StateBot = StateBot.Inspection;
                            _timeRemaining.AddTimeRemaining();
                        }
                    }
                }
            }

            if (Vision.VisionM(transform, Target))
            {
                StateBot = StateBot.Detected;
            }
        }
        else
        {
            if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
            {
                Agent.stoppingDistance = _stoppingDistance;
            }
            if (Vision.VisionM(transform, Target))
            {
                Weapon.Fire();
            }
            else
            {
                MovePoint(Target.position);
            }

            //todo потеря персонажа
        }
    }

    private void ResetStateBot()
    {
        StateBot = StateBot.None;
    }

    private void SetDamage(InfoCollision info)
    {
        //todo реакция на попадание

       if (HP > 0)
        {
            HP -= info.Damage;
        }

       if (HP <= 0)
        {
            StateBot = StateBot.Dead;
            Agent.enabled = false;
            foreach (var child in GetComponentsInChildren<Transform>())
            {
                child.parent = null;

                var tempRbChild = child.GetComponent<Rigidbody>();
                if (!tempRbChild)
                {
                    tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                }
                //tempRbChild.AddForce(info.Dir * Random.Range(10, 300));

                Destroy(child.gameObject, 10);
            }

            OnDieChange?.Invoke(this);
        }
    }

    public void MovePoint(Vector3 point)
    {
        Agent.SetDestination(point);
    }

    #endregion
}
