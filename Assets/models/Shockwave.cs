using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.models {
    public class Shockwave : MonoBehaviour {
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
            this.StartCoroutine( this.DestroyAfterAnimation() );
        }

        void FixedUpdate() {
            
        }

        void OnCollisionEnter2D( Collision2D col ) {
            var go = col.gameObject;
            if ( col.transform == this || go.GetComponent<ItemBase>().Type == ItemType.SHOCKWAVE ) return;
            if ( this._haveShocked.Contains( go ) ) return;
            var center = ( Vector2 )this.transform.position;
            var rb = go.GetComponent<Rigidbody2D>();
            if ( rb != null ) {
                var rbC = ( Vector2 )rb.transform.position;
                var mag = ( center - rbC ).magnitude;
                if ( mag > this.FinalRadius ) mag = this.FinalRadius;
                var distX = rbC.x - center.x;
                var distY = rbC.y - center.y;
                var scaledPower = this.Power * ( 1 - mag / this.FinalRadius );
                Debug.Log( string.Format( "Scaled power: {0}", scaledPower ) );
                var scaledX = distX * scaledPower;
                var scaledY = distY * scaledPower;
                rb.AddForce( new Vector2( scaledX, scaledY ), ForceMode2D.Impulse );
            }
            this._haveShocked.Add( go );
        }

        /*private void ShockSurroundings() {
            var center = ( Vector2 )this.transform.position;
            var radius =
                Mathf.Sqrt( Mathf.Pow( this.transform.localScale.x, 2f ) + Mathf.Pow( this.transform.localScale.y, 2f ) );
            var colliders = Physics2D.OverlapCircleAll( center, radius );
            foreach ( var coldr in colliders ) {
                if ( coldr.transform == this || coldr.GetComponent<ItemBase>().Type == ItemType.SHOCKWAVE ) continue;
                if ( this._haveShocked.Contains( coldr.transform.gameObject ) ) continue;
                var rb = coldr.GetComponent<Rigidbody2D>();
                if ( rb != null ) {
                    var rbC = ( Vector2 )rb.transform.position;
                    var mag = ( center - rbC ).magnitude;
                    if ( mag > this.FinalRadius ) mag = this.FinalRadius;
                    var distX = rbC.x - center.x;
                    var distY = rbC.y - center.y;
                    var scaledPower = this.Power * ( 1 - mag / this.FinalRadius );
                    Debug.Log( string.Format( "Scaled power: {0}", scaledPower ) );
                    var scaledX = distX * scaledPower;
                    var scaledY = distY * scaledPower;
                    rb.AddForce( new Vector2( scaledX, scaledY ), ForceMode2D.Impulse );
                }
                this._haveShocked.Add( coldr.transform.gameObject );
            }
        }*/

        private IEnumerator DestroyAfterAnimation() {
            yield return new WaitForSeconds( this.Anim.length );
            Destroy( this.gameObject );
        }

        #endregion


        #region Fields

        public AnimationClip Anim;

        public float FinalRadius = 10f;
        public float Power = 10f;

        private readonly List<GameObject> _haveShocked = new List<GameObject>();

        #endregion

    }
}
