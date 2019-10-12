using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private LevelController lvControl;
    private List<GameObject> sections;
    private List<GameObject> sectionToDesert;
    private List<GameObject> desertSections;
    private List<GameObject> seaSections;

    private Transform parent;
    private Vector2 up;
    private Quaternion[] quaternions;

    private float xValueStartDesert;
    private float xValueStartSea;

    // Start is called before the first frame update
    void Start()
    {
        lvControl = GameObject.Find("LevelController").GetComponent<LevelController>();
        parent = GameObject.Find("MapSections").transform;

        xValueStartDesert = lvControl.xValueStartDesert;
        xValueStartSea = lvControl.xValueStartSea;

        quaternions = new Quaternion[] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 90), Quaternion.Euler(0, 0, -90), Quaternion.Euler(0, 0, 180) };

        sections = new List<GameObject>();
        sectionToDesert = new List<GameObject>();
        desertSections = new List<GameObject>();
        seaSections = new List<GameObject>();

        for (int n = 0; n < 20; n++)
        {
            sections.Add(Resources.Load("Prefabs/Sections/Section" + n) as GameObject);
        }

        for (int n = 0; n < 20; n++)
        {
            desertSections.Add(Resources.Load("Prefabs/DesertSections/DesertSection" + n) as GameObject);
        }

        for (int n = 0; n < 6; n++)
        {
            sectionToDesert.Add(Resources.Load("Prefabs/SectionToDesert/sectionToDesert" + n) as GameObject);
        }
        for (int n = 0; n < 12; n++)
        {
            seaSections.Add(Resources.Load("Prefabs/SeaSections/SeaSection" + n) as GameObject);
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
            if (point.x  == lvControl.usedPoints[n].x && point.y == lvControl.usedPoints[n].y)
            {
                return true;
            }
        }
        return false;
    }

    private void Generate(Vector3 point, float x, float y)
    {
        Vector3 coordinate = point + new Vector3(x, y, point.z);
        if (!ExistPoint(coordinate))
        {
            if (coordinate.x == xValueStartDesert)
            {
                GeneSection(sectionToDesert, coordinate, false);
            }
            else if (coordinate.x >= xValueStartDesert)
            {
                GeneSection(desertSections, coordinate, false);
            }
            else if (coordinate.x <= xValueStartSea)
            {
                GeneSection(seaSections, coordinate, true);
            }
            else
            {
                GeneSection(sections, coordinate, true);
            }





            //    GameObject section = Instantiate(sections[Random.Range(0, sections.Count)], parent);
            //section.transform.position = coordinate;
            //section.transform.rotation = quaternions[Random.Range(0, 4)];

            lvControl.usedPoints.Add(coordinate);
        }

    }

    private void GeneSection(List<GameObject> sections, Vector3 posi, bool rotate)
    {
        if (rotate)
        {
            Instantiate(sections[Random.Range(0, sections.Count)], posi, quaternions[Random.Range(0, 4)], parent);
        }
        else
        {
            Instantiate(sections[Random.Range(0, sections.Count)], posi, Quaternion.identity, parent);
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
