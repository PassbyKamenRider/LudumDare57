using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance {
      get {
        if (instance == null) {
           instance = FindAnyObjectByType<T>();
           if (instance == null) {
             GameObject obj = new GameObject ();
             obj.name = typeof(T).Name;
             instance = obj.AddComponent<T>();
           }
        }
       return instance;
      }
    }
 
    public virtual void Awake ()
    {
       if (instance == null) {
         instance = this as T;
         DontDestroyOnLoad (this.gameObject);
    } else {
      Debug.LogWarning("Destroying extra " + gameObject.name + " on " + gameObject.scene.name);
      Destroy (gameObject);
    }
  }
}
