using UnityEngine;
using System;
using UnityEngine.AI;


public sealed class Bot : BaseObjectScene, IExecute
{
    #region Fields

    public float HP = 100;
    public Vision Vision;
    public Weapon Weapon;
    public event Action<Bot> OnDieChange;

    [SerializeField] private Transform _lookObj;
    [SerializeField] private Transform _leftHandObj;
    [SerializeField] private Transform _rightHandObj;

    private StateBot _stateBot;
    private Vector3 _point;
    private Animator _animator;
    private float _waitTime = 3;
    private float _stoppingDistance = 2.0f;
    private bool _isPlayerVisible = false;

    private static readonly int _isMoving = Animator.StringToHash("isMoving");
    private static readonly int _fireDisable = Animator.StringToHash("FireDisable");
    private static readonly int _fireEnable = Animator.StringToHash("FireEnable");

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

                case StateBot.Detected:
                    _isPlayerVisible = true;
                    break;
                default:
                    break;
            }
        }
    }

    #endregion


    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _animator.SetBool(_isMoving, true);
        _rightHandObj = FindObjectOfType<CharacterController>().transform;
        Agent = GetComponent<NavMeshAgent>();
        _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
    }

    private void OnEnable()
    {
        var bodyBot = GetComponentInChildren<BodyBot>();
        if (bodyBot != null) bodyBot.OnApplyDamageChange += Damage;

        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange += Damage;
    }

    private void OnDisable()
    {
        var bodyBot = GetComponentInChildren<BodyBot>();
        if (bodyBot != null) bodyBot.OnApplyDamageChange -= Damage;

        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange -= Damage;
    }

    private void OnAnimatorIK()
    {
        if (_isPlayerVisible)
        {
            if (Target != null)
            {
                _animator.SetLookAtWeight(0.4f);
                _animator.SetLookAtPosition(Target.position);
            }

            if (_rightHandObj != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.4f);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.1f);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObj.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObj.rotation);
            }

            if (_leftHandObj != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.1f);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandObj.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandObj.rotation);
            }
        }
    }

    #endregion


    #region Methods

    public void Execute()
    {
        if (StateBot == StateBot.Dead) return;

        _animator.SetBool(_isMoving, Agent.hasPath);

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
                _isPlayerVisible = true;
                if (Weapon.Magazine.CountAmmunition != 0)
                {
                    Weapon.Fire();
                }
                else
                {
                    Weapon.ReloadMag();
                }
            }
            else
            {
                _isPlayerVisible = false;
                MovePoint(Target.position);
            }

            //todo потеря персонажа
        }
    }

    private void ResetStateBot()
    {
        StateBot = StateBot.None;
    }

    public void Damage(InfoCollision info)
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
                tempRbChild.isKinematic = false;
                tempRbChild.AddForce(info.Dir * UnityEngine.Random.Range(10, 30));

                Destroy(child.gameObject, 7);
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
