using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.models {
    public class TokeSelection:MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            var ti = this.GetComponent<TokenInput>();
            ti.SelectedToken += this.ti_SelectedToken;
            ti.DeselectedToken += this.ti_DeselectedToken;
        }

        

        

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        void ti_SelectedToken() {
            this.GetComponent<Renderer>().material.color = Color.magenta;
        }
        void ti_DeselectedToken() {
            this.GetComponent<Renderer>().material.color = this.GetComponent<ItemRendering>().Background;
        }

        #endregion


        #region Fields



        #endregion
    }
}
