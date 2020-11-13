using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    public PlayerActionState CurrentPlayerState
    {
        get { return _CurrentPlayerState; }
        set
        {
            _CurrentPlayerState = value;
            _CurrentAnimationStage = 0;
            AnimationTick();
        }
    }

    public Sprite[] IdleAnimation;
    public AnimationType IdleAnimationType;
    public float IdleAnimationTick;

    public Sprite[] RunningAnimation;
    public AnimationType RunningAnimationType;
    public float RunningAnimationTick;

    private float _PreviousTime;
    private ushort _CurrentAnimationStage = 0;
    private SpriteRenderer _PlayerSpriteRenderer;
    private PlayerActionState _CurrentPlayerState;

    // 0 - Up; 1 - Down
    private bool _EscalationPhase;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerSpriteRenderer = (SpriteRenderer) GetComponentInParent<SpriteRenderer>();
        _PreviousTime = Time.time;
    }

    void AnimationTick()
    {
        Debug.Log(_CurrentAnimationStage);
        _PreviousTime = Time.time;
        switch (CurrentPlayerState) {
            case PlayerActionState.IDLE:
                _PlayerSpriteRenderer.sprite = IdleAnimation[_CurrentAnimationStage];
                break;
            case PlayerActionState.RUNNING:
                _PlayerSpriteRenderer.sprite = RunningAnimation[_CurrentAnimationStage];
                break;

            default:
                break;
        }

        switch (CurrentPlayerState)
        {
            case PlayerActionState.IDLE:
                switch (IdleAnimationType)
                {
                    case AnimationType.ESCALATE:
                        _CurrentAnimationStage += (ushort)1;

                        if (_CurrentAnimationStage > IdleAnimation.Length-1)
                        {
                            _CurrentAnimationStage = (ushort) 0;
                        }

                        break;

                    case AnimationType.DEESCALATE:
                        _CurrentAnimationStage-= (ushort) 1;

                        if (_CurrentAnimationStage < 0 || _CurrentAnimationStage > IdleAnimation.Length - 1)
                        {
                            _CurrentAnimationStage = (ushort) (IdleAnimation.Length - 1);
                        }

                        break;

                    case AnimationType.ESCALATE_AND_DEESCALATE:
                        if (!_EscalationPhase)
                        {
                            _CurrentAnimationStage += (ushort) 1;

                            if (_CurrentAnimationStage == IdleAnimation.Length - 1)
                            {
                                _CurrentAnimationStage = (ushort) (IdleAnimation.Length - 1);
                                _EscalationPhase = true;
                            }
                        }
                        else
                        {
                            _CurrentAnimationStage -= (ushort) 1;

                            if (_CurrentAnimationStage == 0 || _CurrentAnimationStage > IdleAnimation.Length - 1)
                            {
                                _CurrentAnimationStage = (ushort) 0;
                                _EscalationPhase = false;
                            }
                        }

                        break;

                    default:
                        break;
                }
                break;

            case PlayerActionState.RUNNING:
                switch (RunningAnimationType)
                {
                    case AnimationType.ESCALATE:
                        _CurrentAnimationStage += (ushort) 1;

                        if (_CurrentAnimationStage > RunningAnimation.Length - 1)
                        {
                            _CurrentAnimationStage = (ushort) 0;
                        }

                        break;

                    case AnimationType.DEESCALATE:
                        _CurrentAnimationStage -= (ushort) 1;

                        if (_CurrentAnimationStage < 0 || _CurrentAnimationStage > IdleAnimation.Length - 1)
                        {
                            _CurrentAnimationStage = (ushort) (RunningAnimation.Length - 1);
                        }

                        break;

                    case AnimationType.ESCALATE_AND_DEESCALATE:
                        if (!_EscalationPhase)
                        {
                            _CurrentAnimationStage += (ushort)1;

                            if (_CurrentAnimationStage == RunningAnimation.Length - 1)
                            {
                                _EscalationPhase = true;
                            }
                        }
                        else
                        {
                            _CurrentAnimationStage -= 1;

                            if (_CurrentAnimationStage == 0)
                            {
                                _EscalationPhase = false;
                            }
                        }

                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentPlayerState) 
        {
            case PlayerActionState.IDLE:
                if (Time.time - _PreviousTime > IdleAnimationTick)
                {
                    AnimationTick();
                }
                break;
            case PlayerActionState.RUNNING:
                if (Time.time - _PreviousTime > RunningAnimationTick)
                {
                    AnimationTick();
                }
                break;
            default:
                break;

        }
        
    }
}
public enum PlayerActionState
{
    IDLE, 
    RUNNING
}
public enum AnimationType
{
    ESCALATE,
    DEESCALATE,
    ESCALATE_AND_DEESCALATE
}