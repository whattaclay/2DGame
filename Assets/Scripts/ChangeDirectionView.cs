using UnityEngine;

public class ChangeDirectionView : MonoBehaviour
{
    public static float Flip(Transform currentUnit,float flipValue) //переворачивает модельку игрока
    {
        currentUnit.transform.Rotate(0f,180f,0f);
        flipValue *= -1;
        return flipValue;
    }
}