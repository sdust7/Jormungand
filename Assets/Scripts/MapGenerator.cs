using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private LevelController lvControl;
    private List<GameObject> sections;
    private Transform parent;
    private Vector2 up;
    private Quaternion[] quaternions;

    // Start is called before the first frame update
    void Start()
    {
        quaternions = new Quaternion[] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0,-90), Quaternion.Euler(0, 0, 180) };

        sections = new List<GameObject>();
        parent = GameObject.Find("MapSections").transform;
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        for (int n = 0; n < 20; n++) {
            sections.Add(Resources.Load("Prefabs/Sections/Section"+n) as GameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool ExistPoint(Vector3 point)
    {
        for (int n = 0; n < lvControl.usedPoints.Count; n++)
        {
            if (point == lvControl.usedPoints[n])
            {
                return true;
            }
        }
        return false;
    }

    private void Generate(Vector3 point,float x, float y)
    {
        Vector3 coordinate = point + new Vector3(x,y,point.z);
        if (!ExistPoint(coordinate))
        {
            GameObject section = Instantiate(sections[Random.Range(0, sections.Count)],parent);
            section.transform.position = coordinate;
            section.transform.rotation = quaternions[Random.Range(0, 4)];
            lvControl.usedPoints.Add(coordinate);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
        {
            Generate(transform.position, -40.0f, 40.0f);
            Generate(transform.position, -40.0f, 0.0f);
            Generate(transform.position, -40.0f, -40.0f);
            Generate(transform.position, 0.0f, 40.0f);
            Generate(transform.position, 0.0f, -40.0f);
            Generate(transform.position, 40.0f, 40.0f);
            Generate(transform.position, 40.0f, 0.0f);
            Generate(transform.position, 40.0f, -40.0f);

        }
    }
}
