using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{
    public string attackName;
    public int damage;
    public float stunDuration;
    public int knockbackThreshold;
    public Vector2 knockbackForce;
    public float knockbackDuration;
    public float gravityScaleKnockback;

}
