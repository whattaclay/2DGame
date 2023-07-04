using System.Collections;
using UnityEngine;

public class DestroyFromScene: MonoBehaviour
{
    [SerializeField] private float destroyAfter;
    private void OnEnable()
    {
        StartCoroutine(DestroyAfter(destroyAfter)); 
    }
    private IEnumerator DestroyAfter(float destroyTime) // дестроит объект после создания(вешаю на эффекты)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}