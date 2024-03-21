using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class PushBoxCamera : MonoBehaviour
    {
        [SerializeField] public Vector2 topLeft;
        [SerializeField] public Vector2 bottomRight;
        [SerializeField] public GameObject player;

        private void Start()
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            // var targetPosition = this.player.transform.position;
            // var cameraPosition = this.transform.position;

            // if (targetPosition.y >= cameraPosition.y + topLeft.y)
            // {
            //     cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - topLeft.y, cameraPosition.z);
            // }

            // if (targetPosition.y <= cameraPosition.y + bottomRight.y)
            // {
            //     cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - bottomRight.y, cameraPosition.z);
            // }

            // if (targetPosition.x >= cameraPosition.x + bottomRight.x)
            // {
            //     cameraPosition = new Vector3(targetPosition.x - bottomRight.x, cameraPosition.y, cameraPosition.z);
            // }

            // if (targetPosition.x <= cameraPosition.x + topLeft.x)
            // {
            //     cameraPosition = new Vector3(targetPosition.x- topLeft.x, cameraPosition.y, cameraPosition.z);
            // }

            // this.transform.position = cameraPosition;
        }
    }
}