﻿using Common.Data;
using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIWindow {

	public Text title;
	public Text money;
	public GameObject shopItem;
	ShopDefine shop;
	public Transform[] itemRoot;

	void Start()
    {
		StartCoroutine(InitItems());
    }
	IEnumerator InitItems()
    {
		foreach(var kv in DataManager.Instance.ShopItems[shop.ID])
        {
            if (kv.Value.Status > 0)
            {
				GameObject go = Instantiate(shopItem, itemRoot[0]);
				UIShopItem ui = go.GetComponent<UIShopItem>();
				ui.SetShopItem(kv.Key, kv.Value, this);
            }
        }

		yield return null;
    }
	public void SetShop(ShopDefine shop)
    {
		this.shop = shop;
		this.title.text = shop.Name;
		this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
    }
	private UIShopItem selectedItem;
	public void SelectShopItem(UIShopItem item)
    {
		if (selectedItem != null)
			selectedItem.Selected = false;
		selectedItem = item;
    }
	public void OnClickBuy()
    {
        if (selectedItem != null)
        {
			MessageBox.Show("请选择购买的道具", "购买提示");
        }
		if (!ShopManager.Instance.BuyItem(this.shop.ID, this.selectedItem.ShopItenID))
		{
			MessageBox.Show("道具无法购买", "购买提示");
			return;
		}
    }
}
