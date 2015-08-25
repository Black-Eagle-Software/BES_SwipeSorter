using UnityEngine;

namespace Assets.models {
    public class BombInput : MonoBehaviour {
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

        

        

        void OnMouseUp() {
            Debug.Log( "Dropped the bomb!" );
            var center = this.transform.position;

            this.OnBombBoomed(center);
        }

        protected virtual void OnBombBoomed(Vector2 position) {
            var handler = this.BombBoomed;
            if ( handler != null ) {
                handler(position);
            }
        }

        #endregion


        #region Fields

        public delegate void BombBoom(Vector2 position);
        public event BombBoom BombBoomed;

        #endregion

        
    }
}
