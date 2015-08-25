using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class TokenCollision : MonoBehaviour {
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
            //Debug.Log( string.Format( "{0} touched something: {1}!", this.name, col.gameObject.name ) );
            var go = col.gameObject;
            if ( go.GetComponent<ItemBase>() == null ) return;
            switch ( go.GetComponent<ItemBase>().Type ) {
                case ItemType.BLOCKER:
                    /*if ( this.CheckColorMatch( go.GetComponent<ItemRendering>() ) ) {
                        //we're the same color
                        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.collider);
                    }*/
                    break;
                case ItemType.BOUNDARY:
                    //the ram has touched the wall!
                    if ( this.CheckColorMatch( go.GetComponent<ItemRendering>() ) ) {
                        //we're the same color
                        this.OnHitBoundary( this.gameObject );
                    }
                    break;
                case ItemType.TOKEN:
                    break;
            }
        }

        private bool CheckColorMatch( ItemRendering target ) {
            return target.Background == this.GetComponent<ItemRendering>().Background;
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
