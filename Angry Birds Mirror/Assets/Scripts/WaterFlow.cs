using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class WaterFlow : MonoBehaviour
{
    [SerializeField] float wait_time;
    BuoyancyEffector2D buoyancy_effector;
    int i = 0;
    private void Awake()
    {
        buoyancy_effector = GetComponent<BuoyancyEffector2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        run_swtich();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void run_swtich()
    {
        for (int j = 0; j < 19; j ++)
            StartCoroutine(switch_water());
    }
    IEnumerator switch_water()
    {
        if (i % 2 == 0)
        {
            yield return new WaitForSeconds(wait_time); //just making it so I change change wait time in unity without needing to come back here
            buoyancy_effector.flowVariation = -1; //changes direction of flow
            buoyancy_effector.flowMagnitude = -1; //changes magnitude of flow
        } else {
            yield return new WaitForSeconds(wait_time); //just making it so I change change wait time in unity without needing to come back here
            buoyancy_effector.flowVariation = 1; //changes direction of flow
            buoyancy_effector.flowMagnitude = 1; //changes magnitude of flow
        }
        i++;
    }

 

}
     
