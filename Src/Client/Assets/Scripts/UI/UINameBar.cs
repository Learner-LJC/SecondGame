﻿using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINameBar : MonoBehaviour {

    public Text avaverName;
    public Character character;

    //public Text avaverName1;
    //public Character character1;

    // Use this for initialization
    void Start () {
		if(this.character!=null)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        this.UpdateInfo();

        this.transform.forward = Camera.main.transform.forward;

        //this.UpdateInfo1();
        //this.transform.forward = Camera.main.transform.forward;
    }

    //void UpdateInfo1()
    //{
    //    if (this.character1!=null)
    //    {
    //        string name = this.character1.Name + "Lv" + this.character1.Info.Level;
    //        if (name!=this.avaverName1.text)
    //        {
    //            this.avaverName1.text = name;
    //            if (name!=this.avaverName1.text)
    //            {
    //                this.avaverName1.text = name;
    //            }
    //        }
    //    }
    //}
    void UpdateInfo()
    {
        if (this.character != null)
        {
            string name = this.character.Name + " Lv." + this.character.Info.Level;
            if(name != this.avaverName.text)
            {
                this.avaverName.text = name;
            }
        }
    }
}
