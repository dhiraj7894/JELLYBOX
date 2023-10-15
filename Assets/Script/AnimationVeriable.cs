using UnityEngine;
public class AnimationVeriable {
    public const string SPEED = "speed";
    public const string DRAWWEAPON = "drawWeapon";
    public const string SHEATHWEAPON = "sheathWeapon";
    public const string ATTACK = "attack";
    public const string HEAVYATTACK = "heavyAttack";
    public const string MOVE = "move";
    public const string JUMP = "Jump";
    public const string BLOCK = "block";
    public const string HIT = "hit";
    public const string HEAVYHIT = "heavyHit";

    public const string DEATH = "death";

    public AnimationVeriable(){
      Animator.StringToHash(SPEED);
      Animator.StringToHash(DRAWWEAPON);
      Animator.StringToHash(SHEATHWEAPON);
      Animator.StringToHash(ATTACK);
      Animator.StringToHash(HEAVYATTACK);
      Animator.StringToHash(MOVE);
      Animator.StringToHash(JUMP);
      Animator.StringToHash(BLOCK);
      Animator.StringToHash(HIT);
      Animator.StringToHash(HEAVYHIT);
      Animator.StringToHash(DEATH);
   }

}

