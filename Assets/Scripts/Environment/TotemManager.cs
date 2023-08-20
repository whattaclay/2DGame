using UnityEngine;

namespace Environment
{
     public class TotemManager : MonoBehaviour
     {
          public Totem[] totems;
          public int TotemsActivated { get; private set; } = 0;

          private void Awake()
          {
               totems = GetComponentsInChildren<Totem>();
          }

          private void OnEnable()
          {
               foreach (var totem in totems)
               {
                    totem.OnActivated += TotemsCounter;
               }
          }
          private void OnDisable()
          {
               foreach (var totem in totems)
               {
                    totem.OnActivated -= TotemsCounter;
               }
          }
          public void TotemsCounter()
          {
               TotemsActivated += 1;
          }
     }
}