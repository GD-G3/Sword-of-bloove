using UnityEngine;

public enum HitResult { Hit, Blocked, Parried }

/// <summary>
/// Punto único donde llegan los ataques físicos al jugador
/// </summary>
public class PlayerHitReceiver : MonoBehaviour
{
    private PlayerParry parry;
    private PlayerDefense defense;

    private void Awake()
    {
        parry = GetComponent<PlayerParry>();
        defense = GetComponent<PlayerDefense>();
    }

    public HitResult ReceivePhysicalHit()
    {
        if (parry != null && parry.IsParrying)
        {
            Debug.Log("Golpe esquivado");
            return HitResult.Parried;
        }

        if (defense != null && defense.IsDefending)
        {
            Debug.Log("Golpe bloqueado");
            return HitResult.Blocked;
        }

        Debug.Log("Daño recibido");
        return HitResult.Hit;
    }
}
