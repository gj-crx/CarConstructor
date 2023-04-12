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
        [Range(0.0f, 1.0f)]
        public float DampingRatio = 1f;


        private void Start() => Init();

        private async void Init()
        {
            if (gameObject.scene.name == "CarConstructorScene") return;

            await Task.Delay(100);

            transform.localEulerAngles = new Vector3(0, 0, Angle);

            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            transform.localEulerAngles = new Vector3(0, 0, Angle);
            joint.connectedBody = transform.parent.GetComponent<Rigidbody2D>();

            transform.localEulerAngles = new Vector3(0, 0, Angle);

            joint.dampingRatio = DampingRatio;
        }

        
    }
}
