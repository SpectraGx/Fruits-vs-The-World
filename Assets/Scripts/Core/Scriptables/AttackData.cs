using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{
    public string attackName;
    public int damage;
    public float stunDuration;
    public Vector2 knockback;
    public int maxComboHits; // Num max de golpes antes de aplicar knockback
}
