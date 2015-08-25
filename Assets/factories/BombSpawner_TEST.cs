using Assets.models;
using UnityEngine;

namespace Assets.factories {
    public class BombSpawner_TEST : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.SpawnBomb( new Vector2( 0, 0 ) );
        }

        // Update is called once per frame
        void Update() {
            if ( Input.GetButtonDown( "Fire2" ) ) {
                //RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                this.SpawnBomb( new Vector2( ray.origin.x, ray.origin.y ) );
            }
        }

        private void SpawnBomb( Vector2 position ) {
            var b = ( GameObject )Instantiate( this.BombObject, position, Quaternion.identity );
            b.GetComponent<ItemBase>().Type = ItemType.BOMB;
        }

        #endregion


        #region Fields

        public GameObject BombObject;

        #endregion

    }
}
