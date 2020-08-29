using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIWindow {

	public Text money;
	public Transform[] pages;
	public GameObject bagItem;
	List<Image> slots;
	// Use this for initialization
	void Start () {
        
        if (slots == null)
        {
			slots = new List<Image>();
			for(int page = 0; page < this.pages.Length; page++)
            {
				slots.AddRange(this.pages[page].GetComponentsInChildren<Image>(true));
            }
        }
		money.text=User.Instance.CurrentCharacter.Gold.ToString();
		StartCoroutine(InitBags());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator InitBags()
    {
		for(int i = 0; i<BagManager.Instance.Items.Length; i++)
        {
			var item = BagManager.Instance.Items[i];
			if (item.ItemId>0)
			{
				GameObject go = Instantiate(bagItem, slots[i].transform);
				var ui = go.GetComponent<UIIconItem>();
				var def = ItemManager.Instance.Items[item.ItemId].itemDefine;
				ui.SetMainIcon(def.Icon, item.Count.ToString());
			}
        }
		for(int i = BagManager.Instance.Items.Length; i < slots.Count; i++)
        {
			slots[i].color = Color.green;
        }
		yield return null;
    }
	public void SetTile(string title)
    {

    }
    void Clear()
	{
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i].transform.childCount > 0)
			{
				Destroy(slots[i].transform.GetChild(0).gameObject);
			}
		}
	}
	//每次整理背包都先销毁在重新生成
	public void OnReset()
	{
		BagManager.Instance.Reset();
		Clear();
		StartCoroutine(InitBags());
    }
}
