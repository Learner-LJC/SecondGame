using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using UnityEngine;


    class MinimapManager : Singleton<MinimapManager>
    {
        //
        private UIMinimap minimap;
        public UIMinimap Minimap
        {
            get { return minimap; }
            set {
                minimap = value;
                Debug.LogWarningFormat("MinimapManager.Instance.Minimap[{0}] Set", minimap.GetInstanceID());
            }
        }
        //
        private Collider minimapBoundingBox;
        public Collider MinimapBounldingBox
        {
            get { return minimapBoundingBox; }
        }
        public Transform PlayerTransform
        {
            get
            {
                if (User.Instance.CurrentCharacterObject==null)
                    return null;
                return User.Instance.CurrentCharacterObject.transform;

            }

        }

        public Sprite LoadSpriteMinimap()
        {
            return Resloader.Load<Sprite>("UI/Minimap/" + User.Instance.CurrentMapData.MiniMap);
        }

        public void UpdateMinimap(Collider minimapBoundingBox)
        {
            this.minimapBoundingBox = minimapBoundingBox;
            if (this.minimap!=null)
            {
                this.minimap.UpdateMap();
            }
        }
    }

