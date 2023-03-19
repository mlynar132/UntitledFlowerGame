using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject {
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;

    [Header("INPUT")]
    [Tooltip("Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
    public bool SnapInput = true;

    [Tooltip("Minimum input req'd before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
    public float VerticalDeadzoneThreshold = 0.3f;

    [Tooltip("Minimum input req'd before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
    public float HorizontalDeadzoneThreshold = 0.1f;

    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float MaxSpeed = 14;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float Acceleration = 120;

    [Tooltip("The pace at which the player comes to a stop")]
    public float GroundDeceleration = 60;

    [Tooltip("Deceleration in air only after stopping input mid-air")]
    public float AirDeceleration = 30;

    [Tooltip("A constant downward force applied while grounded. Helps on slopes"), Range(0f, -10f)]
    public float GroundingForce = -1.5f;

    [Tooltip("The improved deceleration after landing without input. Helps accuracy while platforming. To disable, set to 1"), Range(1f, 10f)]
    public float StickyFeetMultiplier = 2f;

    [Tooltip("The detection distance for grounding and roof detection"), Range(0f, 0.5f)]
    public float GrounderDistance = 0.05f;

    [Header("JUMP")]
    [Tooltip("Amount of air jumps allowed. e.g. 1 is a standard double jump"), Min(0)]
    public int MaxAirJumps = 1;

    [Tooltip("The immediate velocity applied when jumping")]
    public float JumpPower = 36;

    [Tooltip("The maximum vertical movement speed")]
    public float MaxFallSpeed = 40;

    [Tooltip("The player's capacity to gain fall speed. a.k.a. In Air Gravity")]
    public float FallAcceleration = 110;

    [Tooltip("The gravity multiplier added when jump is released early")]
    public float JumpEndEarlyGravityModifier = 3;

    [Tooltip("The fixed frames before coyote jump becomes unusable. Coyote jump allows jump to execute even after leaving a ledge")]
    public int CoyoteFrames = 7;

    [Tooltip("The amount of fixed frames we buffer a jump. This allows jump input before actually hitting the ground")]
    public int JumpBufferFrames = 7;

    [Header("Stun Ability")]
    [Tooltip("Allows Stun Ability")]
    public bool AllowStunAbility = true;
    //if (AllowStunAbility){
    [Tooltip("Cooldown in seconds")]
    public float StunAbilityCooldown = 1;
    //}
    [Header("Block Ability")]
    [Tooltip("Allows Block Ability")]
    public bool AllowBlockAbility = true;

    [Tooltip("Cooldown in seconds")]
    public float BlockAbilityCooldown = 3;

    [Header("Vine Ability")]
    [Tooltip("Allows Vine Ability")]
    public bool AllowVineAbility = true;

    [Tooltip("Gravity scale while swinging")]
    public float GravityScaleWhileSwinging = 5;

    [Tooltip("movement impact scale while swinging")]
    public float MovementImpactScaleWhileSwinging = 5;

    [Tooltip("Cooldown in seconds")]
    public float VineAbilityCooldown = 3;

    [Tooltip("Max range")]
    public float VineAbilityMaxRange = 3;

    [Header("Grapple")]
    [Tooltip("Allows Grapple")]
    public bool AllowGrapple = true;

    [Tooltip("Cooldown in seconds")]
    public float GrappleCooldown = 3;

    [Header("Bomb")]
    [Tooltip("Allows Bomb")]
    public bool AllowBomb = true;

    [Tooltip("Cooldown in seconds")]
    public float BombCooldown = 3;

    [Header("Anchor")]
    [Tooltip("Allows Anchor")]
    public bool AllowAnchor = true;

    [Tooltip("Cooldown in seconds")]
    public float AnchorCooldown = 3;

    [Header("Dash")]
    [Tooltip("Allows Dash")]
    public bool AllowDash = true;

    [Tooltip("Cooldown in seconds")]
    public float DashCooldown = 3;

    [Tooltip("Dash speed")]
    public float DashVelocity = 50;

    [Tooltip("Dash duration in fixed frames")]
    public float DashDurationFrames = 5;

    [Tooltip("The horizontal speed retained when dash has completed")]
    public float DashEndHorizontalMultiplier = 0.25f;

    [Header("EXTERNAL")]
    [Tooltip("The rate at which external velocity decays. Should be close to Fall Acceleration")]
    public int ExternalVelocityDecay = 100; // This may become deprecated in a future version

#if UNITY_EDITOR
    private void OnValidate() {
        if (PlayerLayer.value <= 1) Debug.LogWarning("Please assign a Player Layer that matches the one given to your Player", this);
    }
#endif
}