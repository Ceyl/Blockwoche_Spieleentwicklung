using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {
    public Transform plate;
    public GameObject buildPlate;
    public float gridSize;

    private BuildMode buildMode;
    private Transform transparentPlate;
    private AimController aimControl;
    private bool canBuild;
    private Dictionary<BuildMode, Vector3> scaleCollection;
    private Vector3 scaleVector;
	// Use this for initialization
	void Start () {
        aimControl = GetComponentInParent<AimController>();
        buildMode = BuildMode.BuildModeOff;
        scaleCollection = new Dictionary<BuildMode, Vector3>();
        scaleCollection[BuildMode.BuildMode2x1] = new Vector3 (2, 1 );
        scaleCollection[BuildMode.BuildMode2x2] = new Vector3 (2, 2 );
        scaleCollection[BuildMode.BuildMode2x4] = new Vector3 (2, 4 );
        scaleCollection[BuildMode.BuildMode4x2] = new Vector3 (4, 2 );
    }
	
	// Update is called once per frame
	void Update () {
        if (buildMode != BuildMode.BuildModeOff)
        {
            SnapToGrid();
            if (Input.GetKeyUp(KeyCode.Mouse1) && canBuild)
            {
                Instantiate(buildPlate, transparentPlate.position, Quaternion.identity);
            }
        }else
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                buildMode = BuildMode.BuildMode2x1;

                GenerateBuild();
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                buildMode = BuildMode.BuildMode2x2;
              
                GenerateBuild();
            }
            plate.localScale = scaleCollection[buildMode];
            buildPlate.transform.localScale = scaleCollection[buildMode];
        }
	}

    private void SnapToGrid()
    {
        float mousePosX = aimControl.mouseCoords.x;
        float mousePosY = aimControl.mouseCoords.y;
        float snapPointX = mousePosX + ((transparentPlate.localScale.x - mousePosX) % transparentPlate.localScale.x);
        float snapPointY = mousePosY + ((transparentPlate.localScale.y - mousePosY) % transparentPlate.localScale.y);
        float snapPointZ = 0;
        Vector3 position = new Vector3(snapPointX, snapPointY, snapPointZ);
        transparentPlate.position = position;
        Collider2D collider = Physics2D.OverlapBox(new Vector2(snapPointX, snapPointY), new Vector2(transparentPlate.localScale.x-1, transparentPlate.localScale.y-1), 0);
        Debug.DrawLine(transparentPlate.position, new Vector3(transparentPlate.position.x, transparentPlate.position.y, -10));
        if (collider != null)
        {
            canBuild = false;
            transparentPlate.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        }
        else
        {
            canBuild = true;
            transparentPlate.GetComponent<Renderer>().material.color = new Color(0.0f, 0.5f, 0.5f, 0.2f);
        }
    }

    private void GenerateBuild()
    {
        Vector3 position = new Vector3(0, 0, 0);
        transparentPlate = Instantiate(plate, position, Quaternion.identity);
        
    }
}
