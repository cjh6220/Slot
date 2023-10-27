using UnityEngine;

/*
    
    25.10.2021 - destroy any gameobject
    27.08.2020 - first
 */
namespace Mkey
{
    public class DelayDestroy : MonoBehaviour
    {
        public float time = 0.0f;
        [Tooltip("Destroy auto this gameobject after Awake.")]
        public bool auto = true;

        void Awake()
        {
            if(auto)  DestroyGameObject();
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject, time);
        }

        public void DestroyGameObject(GameObject gO)
        {
            if (gO) Destroy(gO, time);
        }
    }
}
