using UnityEngine;

public class UnitMovementSystem : MonoBehaviour
{
    public void RegisterUnit(Unit unit)
    {
        Debug.Log("Enviando unidad: " + unit.name);
    }
}
