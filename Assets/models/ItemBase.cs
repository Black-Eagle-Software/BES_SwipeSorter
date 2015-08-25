using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.models {
    public class ItemBase : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties

        //public ItemType Type { get; set; }

        #endregion


        #region Methods

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (!this.GetComponent<ItemRendering>()) return;
            if ( !this.GetComponent<ItemRendering>().ItemRenderer.isVisible ) {
                this.IsDoneInitializing = true;
            }
        }

        void FixedUpdate() {

        }

        #endregion


        #region Fields

        public ItemType Type;

        private bool IsDoneInitializing;

        #endregion


    }
}
