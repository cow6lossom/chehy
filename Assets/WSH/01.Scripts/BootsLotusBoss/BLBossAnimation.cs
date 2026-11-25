using UnityEngine;

public class BossAnimation :  MonoBehaviour
{
    private Animator _aniCompo;

    [Header("Hash")] [SerializeField] private int _idleHash = Animator.StringToHash("IDLE");
    [SerializeField] private int _enterHash = Animator.StringToHash("ENTER");
    [SerializeField] private int _outHash = Animator.StringToHash("OUT");
    [SerializeField] private int _oneSkillHash = Animator.StringToHash("ONESKILL");
    [SerializeField] private int _twoSkillHash = Animator.StringToHash("TWOSKILL");
    [SerializeField] private int _threeSkillHash = Animator.StringToHash("THREESKILL");
    [SerializeField] private int _fourSkillHash = Animator.StringToHash("FOURSKILL");

    private void Awake()
    {
        _aniCompo = GetComponent<Animator>();
    }

    public void Idle()
    {
        Cancel();
        _aniCompo.SetBool(_idleHash, true);
    }

    public void Enter()
    {
        Cancel();
        _aniCompo.SetBool(_enterHash, true);
    }

    public void Out()
    {
        Cancel();
        _aniCompo.SetBool(_outHash, true);
    }

    public void OneSkill()
    {
        Cancel();
        _aniCompo.SetBool(_oneSkillHash, true);
    }

    public void TwoSkill()
    {
        Cancel();
        _aniCompo.SetBool(_twoSkillHash, true);
    }

    public void ThreeSkill()
    {
        Cancel();
        _aniCompo.SetBool(_threeSkillHash, true);
    }

    public void FourSkill()
    {
        Cancel();
        _aniCompo.SetBool(_fourSkillHash, true);
    }

    public void Cancel()
    {
        _aniCompo.SetBool(_idleHash, false);
        _aniCompo.SetBool(_enterHash, false);
        _aniCompo.SetBool(_outHash, false);
        _aniCompo.SetBool(_oneSkillHash, false);
        _aniCompo.SetBool(_twoSkillHash, false);
        _aniCompo.SetBool(_threeSkillHash, false);
        _aniCompo.SetBool(_fourSkillHash, false);
    }
}
