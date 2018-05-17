using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {
    public Transform plate;
    public GameObject buildPlate;
    public Material transparent;
    public Material cantBuildTransparent;
    public Color playerColor;
    private BuildMode buildMode;
    private Transform transparentPlate;
    private AimController aimControl;
    private bool canBuild;
    private Dictionary<BuildMode, Vector3> scaleCollection;
    private Vector3 scaleVector;
    private bool buildModeOn;
    private int selectedBuildMode;
    private bool isGenerating;
    private Color[] colors = new Color[] { Color.blue, Color.red, Color.green, Color.yellow };
    private Dictionary<BuildMode, float> coolDowns;
    // Use this for initialization
    void Start () {
        aimControl = GetComponentInParent<AimController>();
        buildModeOn = false;
        scaleCollection = new Dictionary<BuildMode, Vector3>();
        scaleCollection[BuildMode.BuildMode2x1] = new Vector3 (2, 1 );
        scaleCollection[BuildMode.BuildMode2x2] = new Vector3 (2, 2 );
        scaleCollection[BuildMode.BuildMode2x4] = new Vector3 (2, 4 );
        scaleCollection[BuildMode.BuildMode4x2] = new Vector3 (4, 2 );
        coolDowns = new Dictionary<BuildMode, float>();
        coolDowns[BuildMode.BuildMode2x1] = 0;
        coolDowns[BuildMode.BuildMode2x2] = 0;
        coolDowns[BuildMode.BuildMode2x4] = 0;
        coolDowns[BuildMode.BuildMode4x2] = 0;
        isGenerating = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        ManageInput();
        
        if (buildModeOn)
        {
            SnapToGrid();
            if (Input.GetAxisRaw(gameObject.name + " Build") > 0 && canBuild && !isGenerating && Time.time >= coolDowns[buildMode])
            {
                coolDowns[buildMode] = Time.time + Mathf.Max(transparentPlate.localScale.x, transparentPlate.localScale.y) / 2;
                isGenerating = true;
                buildPlate.transform.localScale = transparentPlate.localScale;
                GameObject cube = Instantiate(buildPlate, transparentPlate.position, Quaternion.identity);
                cube.GetComponent<SpriteRenderer>().material.color = playerColor;
            }
            if (Input.GetAxis(gameObject.name + " Build") <= 0) isGenerating = false;
            if (Input.GetButtonUp(gameObject.name + " Cancel"))
            {
                Destroy(transparentPlate.gameObject);
                buildModeOn = false;
            }
        }

	}

    private void SnapToGrid()
    {
        float mousePosX = aimControl.mouseCoords.x;
        float mousePosY = aimControl.mouseCoords.y;
        float snapPointX = mousePosX + ((1 - mousePosX) % 1);
        float snapPointY = mousePosY + ((1 - mousePosY) % 1);
        float snapPointZ = 0;
        Vector3 position = new Vector3(snapPointX, snapPointY, snapPointZ);
        transparentPlate.position = position;
        Collider2D collider = Physics2D.OverlapBox(new Vector2(snapPointX, snapPointY), new Vector2(transparentPlate.localScale.x-1, transparentPlate.localScale.y-1), 0);

        if (collider != null)
        {
            canBuild = false;
            transparentPlate.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            canBuild = true;
            transparentPlate.gameObject.GetComponent<Renderer>().enabled = true; 
        }
    }

    private void GenerateBuild()
    {
        if (transparentPlate != null) Destroy(transparentPlate.gameObject);
        Vector3 position = new Vector3(0, 0, 0);
        transparentPlate = Instantiate(plate, position, Quaternion.identity);
        transparentPlate.gameObject.GetComponent<SpriteRenderer>().material.color = playerColor;
        
    }

    private void ManageInput()
    {
        if (Input.GetButtonUp(gameObject.name + " SelectBuildLeft"))
        {
            if (buildMode > 0) buildMode = buildMode - 1;
            else buildMode = (BuildMode)Enum.GetNames(typeof(BuildMode)).Length - 1;
            plate.localScale = scaleCollection[buildMode];
            buildPlate.transform.localScale = plate.localScale;
            GenerateBuild();
            buildModeOn = true;
        }
        if (Input.GetButtonUp(gameObject.name + " SelectBuildRight"))
        {
            if ((int)buildMode < Enum.GetNames(typeof(BuildMode)).Length - 1) buildMode = buildMode + 1;
            else buildMode = 0;
            plate.localScale = scaleCollection[buildMode];
            buildPlate.transform.localScale = plate.localScale;
            GenerateBuild();
            buildModeOn = true;
        }



    }

    public void HitPlayer(Vector2 direction, float force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}
