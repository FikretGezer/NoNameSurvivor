using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public class MinimapPositionAdjuster : MonoBehaviour
    {
        void Update()
        {
            Vector3 followedPos = CharacterSpawner.Instance._position;
            transform.position = new Vector3(followedPos.x, transform.position.y, followedPos.z);
        }
    }
}
