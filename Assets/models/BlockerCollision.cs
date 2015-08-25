using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class BlockerCollision : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        void OnCollisionEnter2D( Collision2D col ) {
            var go = col.gameObject;
            switch ( go.GetComponent<ItemBase>().Type ) {
                case ItemType.BLOCKER:
                    break;
                case ItemType.BOUNDARY:
                    break;
                case ItemType.TOKEN:
                    if ( this.CheckColorMatch( go.GetComponent<ItemRendering>() ) ) {
                        //we're the same color
                        //this doesn't work, at all...boooooo :(
                        Physics2D.IgnoreCollision(this.transform.parent.GetComponent<Collider2D>(), col.collider, true);
                    }
                    break;
            }
        }

        private bool CheckColorMatch( ItemRendering target ) {
            return target.Background == this.transform.parent.GetComponent<ItemRendering>().Background;
        }

        protected virtual void OnHitBoundary( GameObject obj ) {
            var handler = this.HitBoundary;
            if ( handler != null ) handler( obj );
        }

        #endregion


        #region Fields

        public delegate void DidHitBoundary( GameObject obj );
        public event DidHitBoundary HitBoundary;

        #endregion
    }
}
