using System.Collections;
using UnityEngine;

namespace Infractructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopAllCoroutines();
    
    void StopCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
  }
}