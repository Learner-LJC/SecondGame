using Common.Data;
using Managers;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NpcController : MonoBehaviour {
	public int NpcID;
	SkinnedMeshRenderer renderer;
	Animator anim;
	NpcDefine npc;
	Color orginColor;
	private bool inInteractive = false;
	// Use this for initialization
	void Start () {
		renderer = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
		anim = this.gameObject.GetComponentInChildren<Animator>();
		orginColor = renderer.sharedMaterial.color;
		npc = NPCManager.Instance.GetNpcDefine(this.NpcID);
		this.StartCoroutine(Actions());
	}
	IEnumerator Actions()
    {
        while (true)
        {
            if (inInteractive)
            {
				yield return new WaitForSeconds(2f);
            }
            else
            {
				yield return new WaitForSeconds(UnityEngine.Random.Range(5f,10f));
            }
			this.Relax();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

	void Relax()
    {
		anim.SetTrigger("Relax");
    }
	void Interactive()
    {
        if (!inInteractive)
        {
			inInteractive = true;
			StartCoroutine(DoInteractive());
        }
    }
	IEnumerator DoInteractive()
    {
		yield return FaceToPlayer();
        if (NPCManager.Instance.Interactive(npc))
        {
			anim.SetTrigger("Talk");
        }
		yield return new WaitForSeconds(3f);
		inInteractive = false;
    }
	IEnumerator FaceToPlayer()
    {
		Vector3 faceTo = (User.Instance.CurrentCharacterObject.transform.position - this.transform.position).normalized;
        while (Math.Abs(Vector3.Angle(this.gameObject.transform.forward,faceTo)) > 5)
        {
			this.gameObject.transform.forward = Vector3.Lerp(this.gameObject.transform.forward, faceTo, Time.deltaTime*5);
			yield return null;
        }
    }
    void OnMouseDown()
    {
		Interactive();
    }
	private void OnMouseOver()
    {
		Highlight(true);
    }
	private void OnMouseEnter()
    {
		Highlight(true);
    }
	private void OnMouseExit()
    {
		Highlight(false);
    }
	void Highlight(bool highlight)
    {
        if (highlight)
        {
			if (renderer.sharedMaterial.color != Color.white)
				renderer.sharedMaterial.color = Color.white;
        }
        else
        {
			if (renderer.sharedMaterial.color != orginColor)
				renderer.sharedMaterial.color = orginColor;
        }
    }

}
