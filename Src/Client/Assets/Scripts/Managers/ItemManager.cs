using Common.Data;
using Models;
using Services;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public class ItemManager : Singleton<ItemManager>
    {

        public Dictionary<int, Item> Items = new Dictionary<int, Item>();
        public void Init(List<NItemInfo> Nitems)
        {
            this.Items.Clear();
		    foreach (var item in Nitems)
		    {
			    Item newItem = new Item(item);
			    this.Items.Add(item.Id, newItem);

			    Debug.LogFormat("ItemManager : Init() : {0}", newItem);
		    }
            StatusService.Instance.ResgisterStatusNotify(StatusType.Item, OnItemNotify);
        }
        public ItemDefine GetItem(int itemId)
        {
            return null;
        }

        bool OnItemNotify(NStatus status)
        {
            if (status.Action == StatusAction.Add)
            {
                this.AddItem(status.Id, status.Value);
            }
            if (status.Action == StatusAction.Delete)
            {
                this.RemoveItem(status.Id, status.Value);
            }
            return true;
        }
        void AddItem(int itemId,int count)
        {
            Item item = null;
            if(this.Items.TryGetValue(itemId,out item))
            {
                item.count += count;
            }
            else
            {
                item = new Item(itemId, count);
                this.Items.Add(itemId, item);
            }
			//背包显示增加的道具
            BagManager.Instance.AddItem(itemId,count);
        }
        void RemoveItem(int itemId,int count)
        {
            if (!this.Items.ContainsKey(itemId))
            {
                return;
            }
            Item item = this.Items[itemId];
			//删除的道具数量少于传入的数量返回
            if (item.count < count)
            {
                return;
            }
            item.count -= count;
            BagManager.Instance.RemoveItem(itemId,count);
        }
        public bool UseItem(int itemId)
        {
            return false;
        }
        public bool UseItem(ItemDefine item)
        {
            return false;
        }
    }
}
