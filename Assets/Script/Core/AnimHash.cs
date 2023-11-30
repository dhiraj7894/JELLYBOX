using TMPro;
using UnityEngine;
public class AnimHash {
    public const string SPEED = "speed";
    public const string ATTACK = "attack";
    public const string HEAVYATTACK = "heavyAtk";
    public const string MOVE = "move";
    public const string JUMP = "Jump";
    public const string BLOCK = "block";
    public const string HIT = "hit";
    public const string HEAVYHIT = "heavyHit";
    public const string SHIELD = "Shield";
    public const string SPECIAL_A = "SP_A";
    public const string ENDSPA = "EndSpecialAttack";
    public const string PSA = "Player Special Attack";

    public const string DEATH = "death";

    public const string ATTACKTYP = "atkType";
    public AnimHash(){
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
        Animator.StringToHash(SPECIAL_A);        
        Animator.StringToHash(ATTACKTYP);        
    }

}

public class TagHash
{
    public const string PLAYER = "Player";

}

