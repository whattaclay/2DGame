using UnityEngine;

namespace Environment
{
     public class TotemManager : MonoBehaviour
     {
          private Totem[] _totems;
          public int TotemsActivated { get; private set; } = 0;
          private void Awake()
          {
               _totems = FindObjectsOfType<Totem>();
          }
          private void OnEnable()
          {
               foreach (var totem in _totems)
               {
                    totem.OnActivated += TotemsCounter;
               }
          }
          private void OnDisable()
          {
               foreach (var totem in _totems)
               {
                    totem.OnActivated -= TotemsCounter;
               }
          }
          private void TotemsCounter()
          {
               TotemsActivated += 1;
          }
     }
}