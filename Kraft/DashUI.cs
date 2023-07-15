using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashUI : MonoBehaviour
{
    public int dashAmount;


    public GameObject dashPrefab;
    public GameObject UIParent;
    List<GameObject> dashIcons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject icon in dashIcons)
        {
            Destroy(icon);
        }
        dashIcons.Clear();

        for (int i = 0; i < dashAmount; i++)
        {
            GameObject newIcon = Instantiate(dashPrefab);
            newIcon.transform.SetParent(UIParent.transform, false);
            dashIcons.Add(newIcon);
        }
    }
}
