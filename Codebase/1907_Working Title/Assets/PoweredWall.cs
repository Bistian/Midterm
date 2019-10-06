using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredWall : MonoBehaviour
{
    [SerializeField] List<GameObject> hearts = null;

    private void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i].GetComponent<MeshRenderer>().enabled == false)
            {
                Destroy(hearts[i]);
                hearts.Remove(hearts[i]);
            }
        }

        if (hearts.Count < 1)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().DoorUnlock();
            Destroy(this.gameObject);
        }
    }
}
