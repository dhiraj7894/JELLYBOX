using UnityEngine;
public class AnimationVeriable {
    public const string SPEED = "speed";
    public const string ATTACK = "attack";
    public const string HEAVYATTACK = "heavyAtk";
    public const string MOVE = "move";
    public const string JUMP = "Jump";
    public const string BLOCK = "block";
    public const string HIT = "hit";
    public const string HEAVYHIT = "heavyHit";
    public const string SHIELD = "Shield";

    public const string DEATH = "death";

    public AnimationVeriable(){
        Animator.StringToHash(SPEED);
        Animator.StringToHash(ATTACK);
        Animator.StringToHash(HEAVYATTACK);
        Animator.StringToHash(MOVE);
        Animator.StringToHash(JUMP);
        Animator.StringToHash(BLOCK);
        Animator.StringToHash(HIT);
        Animator.StringToHash(HEAVYHIT);
        Animator.StringToHash(SHIELD);
        Animator.StringToHash(DEATH);
    }

}

