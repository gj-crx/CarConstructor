using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cars;
using System.Threading.Tasks;

namespace Cars
{
    public class FramePartType : MonoBehaviour
    {
        public float Angle = 0;


        private void Start() => Init();

        private async void Init()
        {
            if (gameObject.scene.name == "CarConstructorScene") return;

            await Task.Delay(100);

            Debug.Log(transform.localEulerAngles);
            transform.localEulerAngles = new Vector3(0, 0, Angle);
            Debug.Log(transform.localEulerAngles);

            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            transform.localEulerAngles = new Vector3(0, 0, Angle);
            joint.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
            transform.localEulerAngles = new Vector3(0, 0, Angle);

            Debug.Log(transform.localEulerAngles + " last ");
        }

        
    }
}
