using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class TokenInput : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties

        

        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.CanTestForInput = true;
        }

        // Update is called once per frame
        void Update() {
            
        }

        void FixedUpdate() {
            
        }

        void OnPress() {
            if ( !this.CanTestForInput ) return;
            //this is stupidly easy compared to hit-casting :(
            Debug.Log( "Hit a token!" );
            this.OnSelectedToken();
            this._isDragging = true;
            this._startDragPosition = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.touches[0].position ), Vector2.zero ).transform.position;
        }

        void OnMouseDown() {
            if ( !this.CanTestForInput ) return;
            //this is stupidly easy compared to hit-casting :(
            Debug.Log( "Hit a token!" );
            this.OnSelectedToken();
            this._isDragging = true;
            this._startDragPosition = Physics2D.Raycast( Camera.main.ScreenToWorldPoint( Input.mousePosition ), Vector2.zero ).transform.position;
        }

        void OnRelease() {
            if ( !this._isDragging ) return;
            //still hit-cast as we need the mouse position
            this._endDragPosition = Camera.main.ScreenToWorldPoint( Input.touches[0].position );

            //get a vector between start and end drag positions; 
            //ignore the y component for now???
            var vect = new Vector2( this._endDragPosition.x - this._startDragPosition.x, /*this._endDragPosition.y - this._startDragPosition.y*/0 );
            var rb2D = this.GetComponent<Rigidbody2D>();
            if ( rb2D != null ) {
                rb2D.AddForce( vect );
            }
            this.OnDeselectedToken();
            this._isDragging = false;
        }

        void OnMouseUp() {
            if ( !this._isDragging ) return;
            //still hit-cast as we need the mouse position
            this._endDragPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
            
            //get a vector between start and end drag positions; 
            //ignore the y component for now???
            var vect = new Vector2( this._endDragPosition.x - this._startDragPosition.x, /*this._endDragPosition.y - this._startDragPosition.y*/0 );
            var rb2D = this.GetComponent<Rigidbody2D>();
            if ( rb2D != null ) {
                rb2D.AddForce( vect );
            }
            this.OnDeselectedToken();
            this._isDragging = false;
        }

        private Vector2 GetVectorCrossProduct( Vector2 start, Vector2 end ) {
            var x = Mathf.Sqrt( Mathf.Pow( this._startDragPosition.x, 2f ) + ( float )Math.Pow( this._endDragPosition.x, 2f ) );
            var y = Mathf.Sqrt( Mathf.Pow( this._startDragPosition.y, 2f ) + ( float )Math.Pow( this._endDragPosition.y, 2f ) );
            return new Vector2( x, y );
        }

        protected virtual void OnSelectedToken() {
            var handler = this.SelectedToken;
            if ( handler != null ) handler();
        }
        protected virtual void OnDeselectedToken() {
            var handler = this.DeselectedToken;
            if ( handler != null ) handler();
        }

        #endregion


        #region Fields

        public bool CanTestForInput;

        public delegate void DidSelectToken();
        public event DidSelectToken SelectedToken;

        public delegate void DidDeselectToken();
        public event DidDeselectToken DeselectedToken;

        private bool _isDragging;
        private Vector2 _startDragPosition;
        private Vector2 _endDragPosition;

        #endregion

        
    }
}
