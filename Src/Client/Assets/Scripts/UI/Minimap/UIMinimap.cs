using System.Collections;
using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour
{
    public Collider MinimapBuildingBox;
    public Image minimap;

    public Image arrow;

    public Text mapName;

    private Transform playerTransform;
	// Use this for initialization
	void Start ()
    {
        Debug.LogWarning("UI Minimap Start" + this.GetInstanceID());
        MinimapManager.Instance.Minimap = this;
        this.UpdateMap();
    }

    public void UpdateMap()
    {
        this.mapName.text = User.Instance.CurrentMapData.Name;
        
        this.minimap.overrideSprite = MinimapManager.Instance.LoadSpriteMinimap();
        this.minimap.SetNativeSize();
        this.minimap.transform.localPosition = Vector3.zero;
        this.MinimapBuildingBox = MinimapManager.Instance.MinimapBounldingBox;
        this.playerTransform = null;
    }
	// Update is called once per frame
	void Update ()
    {
        if (playerTransform==null)
        {
            playerTransform = MinimapManager.Instance.PlayerTransform;
        }
        if (MinimapBuildingBox==null||playerTransform==null) return;
        
        float realWidth = MinimapBuildingBox.bounds.size.x;
        float realHeight = MinimapBuildingBox.bounds.size.z;

        float relaX = playerTransform.position.x - MinimapBuildingBox.bounds.min.x;
        float relaY = playerTransform.position.z - MinimapBuildingBox.bounds.min.z;

        float pivotX = relaX / realWidth;
        float pivotY = relaY / realHeight;

        this.minimap.rectTransform.pivot=new Vector2(pivotX,pivotY);
        this.minimap.rectTransform.localPosition = Vector2.zero;

        this.arrow.transform.eulerAngles=new Vector3(0,0,-playerTransform.eulerAngles.y);
    }
}
