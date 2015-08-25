using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class ItemRendering : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties

        public Renderer ItemRenderer { get { return this._ren; } }

        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._ren = this.GetComponent<SpriteRenderer>();
            this.ItemRenderer.material.color = this.Background;
            this.ItemRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update() {
            if ( !this.ItemRenderer.isVisible ) {
                this.ItemRenderer.enabled = true;
            }
            if ( this.ItemRenderer.material.color != this.Background ) {
                this.ItemRenderer.material.color = this.Background;
            }
        }

        void FixedUpdate() {
            if ( this.ItemRenderer.material.color != this.Background ) {
                this.ItemRenderer.material.color = this.Background;
            }
        }

        #endregion


        #region Fields

        public Color Background;

        private Renderer _ren;

        #endregion
    }
}
