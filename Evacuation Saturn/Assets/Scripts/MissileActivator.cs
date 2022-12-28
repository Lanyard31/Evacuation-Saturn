using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tarodev
{
    public class MissileActivator : MonoBehaviour
    {
        Vector3 targetPosition;
        Vector3 myPosition;
        [SerializeField] private Target _target;
        private float range = 50;
        [SerializeField] Missile _missile;
        [SerializeField] private Rigidbody _rb;
        bool droppedIn = false;
        GameObject parentGameObject;

        void Start()
        {
            _target = FindObjectOfType<Target>();
            parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
            range = Random.Range(80f, 120f);
        }


        void FixedUpdate()
        {
            myPosition = this.transform.position;
            targetPosition = _target.transform.position;
            if ((Vector3.Distance(myPosition, targetPosition) < range) && droppedIn == false)
            {
                _rb.AddRelativeForce(new Vector3(0, -30, Random.Range(70f, 100f)));
                droppedIn = true;
                Invoke("FireMissile", 1f);
            }
        }

        void FireMissile()
        {
            //this.transform.parent = parentGameObject.transform;
            gameObject.AddComponent<Missile>();
            gameObject.AddComponent<SelfDestruct>();
        }
    }
}
