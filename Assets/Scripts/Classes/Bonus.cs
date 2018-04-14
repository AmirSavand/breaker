using UnityEngine;
using System.Collections;

public enum BonusType
{
    Consumable,
    Duration
}

public class Bonus : MonoBehaviour
{
    public BonusType type;

    public Color color;

    public string title;

    public float duration;

    public float amount;
}
