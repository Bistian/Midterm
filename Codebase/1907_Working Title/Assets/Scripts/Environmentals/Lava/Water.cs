using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public float power = 3;
    public float scale = 1;
    public float time = 1;

    private float offsetX;
    private float offsetY;
    public MeshFilter mf;
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        WaterSet();
    }

    // Update is called once per frame
    void Update()
    {
        WaterSet();
        offsetX += Time.deltaTime * time;
        offsetY += Time.deltaTime * time;
    }

    public void WaterSet()
    {
        Vector3[] verticies = mf.mesh.vertices;

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i].y = CalculateHeight(verticies[i].x, verticies[i].z) * power;
        }
        mf.mesh.vertices = verticies;
    }

    float CalculateHeight(float x, float y)
    {
        float xCord = x * scale + offsetX;
        float yCord = y * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}