using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class ItemPosition : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties

        public Vector2 CurrentPosition { get { return this._currentPosition; } }
        public Vector2 PreviousPosition { get { return this._previousPosition; } }

        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.ItemGameObject = this.gameObject;
            this._currentPosition = this.transform.position;
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            this._previousPosition = this.CurrentPosition;
            this._currentPosition = this.transform.position;
            this.OnItemDidMove( this.ItemGameObject, this.CurrentPosition, this.PreviousPosition );
            if ( this.CurrentPosition.y <= this.yMin ) {
                this.OnItemIsOffScreen( this.ItemGameObject );
            }
        }

        protected virtual void OnItemDidMove( GameObject obj, Vector2 newPosition, Vector2 oldPosition ) {
            var handler = this.ItemDidMove;
            if ( handler != null ) handler( obj, newPosition, oldPosition );
        }
        protected virtual void OnItemIsOffScreen( GameObject obj ) {
            var handler = this.ItemIsOffScreen;
            if ( handler != null ) handler( obj );
        }

        #endregion


        #region Fields

        public GameObject ItemGameObject;
        public float yMin;

        public delegate void DidMove( GameObject obj, Vector2 newPosition, Vector2 oldPosition );
        public event DidMove ItemDidMove;

        public delegate void DidMovePastYmin( GameObject obj );
        public event DidMovePastYmin ItemIsOffScreen;

        private Vector2 _currentPosition;
        private Vector2 _previousPosition;

        #endregion
    }
}
