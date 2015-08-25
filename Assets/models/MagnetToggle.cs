using UnityEngine;

namespace Assets.models {
    public class MagnetToggle : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.MagnetAttracts = true;
        }

        // Update is called once per frame
        void Update() {
            
        }

        void FixedUpdate() {
            if ( Input.GetButton( "Fire2" ) ) {
                this.MagnetAttracts = !this.MagnetAttracts;
            }
            if (Input.GetKeyDown(KeyCode.B)) {
                this.GetComponent<ItemRendering>().Background=new Color(0f,0f,1f);
            }
            if ( Input.GetKeyDown( KeyCode.R ) ) {
                this.GetComponent<ItemRendering>().Background = new Color( 1f, 0f, 0f );
            }

            if ( this.MagnetActivated ) {
                if ( this.MagnetAttracts ) {
                    this.Attract();
                } else {
                    this.Repel();
                }
            }
        }

        void OnMouseDown() {
            if ( Input.GetButton( "Fire1" ) ) {
                this.MagnetActivated = !this.MagnetActivated;
            }
        }

        void Attract() {
            var center = ( Vector2 )this.transform.position;
            var colliders = Physics2D.OverlapCircleAll( center, this.PullRadius );
            foreach ( var coldr in colliders ) {
                if ( coldr.transform == this || coldr.GetComponent<ItemBase>().Type != ItemType.TOKEN ) continue;
                if ( coldr.GetComponent<ItemRendering>().Background != this.GetComponent<ItemRendering>().Background ) continue;
                var rb = coldr.GetComponent<Rigidbody2D>();
                if ( rb != null ) {
                    var rbC = ( Vector2 )rb.transform.position;
                    var distX = center.x - rbC.x;
                    var distY = center.y - rbC.y;
                    rb.AddForce( new Vector2( distX, distY ), ForceMode2D.Force );
                }
            }
        }

        private void Repel() {
            var center = ( Vector2 )this.transform.position;
            var colliders = Physics2D.OverlapCircleAll( center, this.PullRadius );
            foreach ( var coldr in colliders ) {
                if ( coldr.transform == this || coldr.GetComponent<ItemBase>().Type != ItemType.TOKEN ) continue;
                if ( coldr.GetComponent<ItemRendering>().Background != this.GetComponent<ItemRendering>().Background ) continue;
                var rb = coldr.GetComponent<Rigidbody2D>();
                if ( rb != null ) {
                    var rbC = ( Vector2 )rb.transform.position;
                    var mag = ( center - rbC ).magnitude;
                    if ( mag > this.PullRadius ) mag = this.PullRadius;
                    var distX = rbC.x - center.x;
                    var distY = rbC.y - center.y;
                    //falls off with the square of the distance
                    var scaledPower = this.RepelForce * ( 1 - ( Mathf.Pow( mag / this.PullRadius, 2f ) ) );
                    Debug.Log( string.Format( "Scaled power: {0}", scaledPower ) );
                    var scaledX = distX * scaledPower;
                    var scaledY = distY * scaledPower;
                    rb.AddForce( new Vector2( scaledX, scaledY ), ForceMode2D.Force );
                }
            }
        }

        #endregion


        #region Fields

        public float PullRadius = 20f;
        public float RepelForce = 1f;
        public bool MagnetActivated;

        public bool MagnetAttracts;

        #endregion

    }
}
