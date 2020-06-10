using UnityEngine;
using System;
using UnityEngine.AI;
using System.Threading;
using UnityEngine.Assertions.Must;

public sealed class Bot : BaseObjectScene, IExecute
{
    #region Fields

    public float HP = 100.0f;
    public Vision Vision;
    public Weapon Weapon;
    public event Action<Bot> OnDieChange;

    [SerializeField] private LayerMask _rayLayer;
    [SerializeField] private Vector3 _leftFootOffset;
    [SerializeField] private Vector3 _rightFootOffset;
    [SerializeField] private Transform _lookObj;
    [SerializeField] private Transform _leftHandObj;
    [SerializeField] private Transform _rightHandObj;
    [SerializeField] private float _rayLength = 1.0f;

    private StateBot _stateBot;
    private Vector3 _point;
    private Vector3 _rFpos;
    private Vector3 _lFpos;
    private Vector3 _rHpos;
    private Vector3 _lHpos;
    private Quaternion _rFrot;
    private Quaternion _lFrot;
    private Animator _animator;
    private Transform _footR;
    private Transform _footL;
    private Transform _handL;
    private Transform _handR;
    private float _weightFootR = 1.0f;
    private float _weightFootL = 1.0f;
    private float _smoothness = 0.5f;
    private float _offsetY = 0.1f;
    private float _waitTime = 3.0f;
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
        _footR = _animator.GetBoneTransform(HumanBodyBones.RightFoot);
        _footL = _animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        _handL = _animator.GetBoneTransform(HumanBodyBones.RightHand);
        _handR = _animator.GetBoneTransform(HumanBodyBones.LeftHand);
        Agent = GetComponent<NavMeshAgent>();
        _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
    }

    private void OnEnable()
    {
        var bodyBot = GetComponentsInChildren<BodyBot>();
        for (int i = 0; i < bodyBot.Length; i++)
        {
            if (bodyBot[i] != null)
            {
                bodyBot[i].OnApplyDamageChange += Damage;
            }
        }


        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange += Damage;
    }

    private void OnDisable()
    {
        var bodyBot = GetComponentsInChildren<BodyBot>();
        for (int i = 0; i < bodyBot.Length; i++)
        {
            if (bodyBot[i] != null)
            {
                bodyBot[i].OnApplyDamageChange -= Damage;
            }
        }

        var headBot = GetComponentInChildren<HeadBot>();
        if (headBot != null) headBot.OnApplyDamageChange -= Damage;
    }

    private void OnAnimatorIK()
    {
        //if (_isPlayerVisible)
        //{
            if (Target != null)
            {
                _animator.SetLookAtWeight(0.4f);
                _animator.SetLookAtPosition(Target.position);
            }

            //_rHpos = _animator.GetIKPosition(AvatarIKGoal.RightHand);
            if (_rightHandObj != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.4f);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.1f);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObj.position);
                _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObj.rotation);
                //_rHpos = Vector3.Lerp(_handR.position, _rightHandObj.position, _smoothness);
            }

            //_lHpos = _animator.GetIKPosition(AvatarIKGoal.LeftHand);
            if (_leftHandObj != null)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.1f);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandObj.position);
                _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandObj.rotation);
                //_rHpos = Vector3.Lerp(_handL.position, _leftHandObj.position, _smoothness);
            }
        //}

        _weightFootR = _animator.GetFloat("Right_Leg");
        _weightFootL = _animator.GetFloat("Left_Leg");

        //LegsIK();

        //_weightFootR = _animator.GetFloat("Right_Leg");
        //_weightFootL = _animator.GetFloat("Left_Leg");

        //_animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _weightFootR);
        //_animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _weightFootL);

        //_animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _weightFootR);
        //_animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _weightFootL);

        //_animator.SetIKPosition(AvatarIKGoal.RightFoot, _rFpos + new Vector3(0, _offsetY, 0));
        //_animator.SetIKPosition(AvatarIKGoal.LeftFoot, _lFpos + new Vector3(0, _offsetY, 0));

        //_animator.SetIKRotation(AvatarIKGoal.RightFoot, _rFrot);
        //_animator.SetIKRotation(AvatarIKGoal.LeftFoot, _lFrot);
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
        }

        //var rPos = _footR.TransformPoint(Vector3.zero);
        //if (Physics.Raycast(rPos, Vector3.down, out var rightHit, _rayLength, _rayLayer))
        //{
        //    _rFpos = Vector3.Lerp(_footR.position, rightHit.point, _smoothness);
        //    _lFrot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        //}
        //var lpos = _footL.TransformPoint(Vector3.zero);
        //if (Physics.Raycast(lpos, Vector3.down, out var leftHit, _rayLength, _rayLayer))
        //{
        //    _lFpos = Vector3.Lerp(_footL.position, leftHit.point, _smoothness);
        //    _lFrot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
        //}
    }

    private void LegsIK()
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _weightFootL);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _weightFootL);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _weightFootR);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _weightFootR);

        RaycastHit hit;
        _lFpos = _animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        if (Physics.Raycast(_lFpos + Vector3.up, Vector3.down, out hit, 2.0f, _rayLayer))
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + _leftFootOffset);
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(_footL.forward, hit.normal)));
            _lFpos = Vector3.Lerp(_footL.position, hit.point, _smoothness);
        }

        _rFpos = _animator.GetIKPosition(AvatarIKGoal.RightFoot);
        if (Physics.Raycast(_rFpos + Vector3.up, Vector3.down, out hit, 2.0f, _rayLayer))
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + _rightFootOffset);
            _animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(Vector3.ProjectOnPlane(_footR.forward, hit.normal)));
            _rFpos = Vector3.Lerp(_footL.position, hit.point, _smoothness);
        }
    }

    private void ResetStateBot()
    {
        StateBot = StateBot.None;
    }

    public void Damage(InfoCollision info)
    {
        if (HP > 0)
        {
            HP -= info.Damage;
            Debug.Log(HP);
        }

        if (HP <= 0)
        {
            StateBot = StateBot.Dead;
            Agent.enabled = false;

            var bodies = GetComponentsInChildren<Rigidbody>();
            foreach (var body in bodies)
            {
                body.isKinematic = false;
            }
            _animator.enabled = false;

            Destroy(gameObject, 7);

            //foreach (var child in GetComponentsInChildren<Transform>())
            //{
            //    child.parent = null;

            //    var tempRbChild = child.GetComponent<Rigidbody>();
            //    if (!tempRbChild)
            //    {
            //        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
            //    }
            //    tempRbChild.isKinematic = false;
            //    tempRbChild.AddForce(info.Dir * UnityEngine.Random.Range(10, 30));

            //    
            //}

            OnDieChange?.Invoke(this);
        }
    }

    public void MovePoint(Vector3 point)
    {
        Agent.SetDestination(point);
    }

    #endregion
}
