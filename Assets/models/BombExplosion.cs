using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.models {
    public class BombExplosion : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.GetComponent<BombInput>().BombBoomed += this.Boom;
        }

        // Update is called once per frame
        void Update() {

        }

        void Boom( Vector2 center ) {
            var colliders = Physics2D.OverlapCircleAll( center, this.Radius );
            var bombs = (from coldr in colliders 
                         where coldr.transform != this 
                         where (coldr.GetComponent<ItemBase>()!=null && coldr.GetComponent<ItemBase>().Type == ItemType.BOMB )
                         select coldr.transform.gameObject).ToList();
            Destroy( this.transform.gameObject );
            var s = (GameObject) Instantiate(this.Shockwave, center, Quaternion.identity);
            s.GetComponent<ItemBase>().Type=ItemType.SHOCKWAVE;
            
            foreach ( var bomb in bombs ) {
                bomb.GetComponent<BombExplosion>().Boom( bomb.transform.position );
            }
        }

        #endregion


        #region Fields

        public float Power;
        public float Radius;
        public GameObject Shockwave;

        #endregion

    }
}
