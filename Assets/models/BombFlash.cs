using UnityEngine;

namespace Assets.models {
    public class BombFlash : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._ren = this.GetComponent<ItemRendering>();
            this._flashingClock = Time.time;
            this._clockStartTime = this._flashingClock;
            this._clockInterval = this._slowFlashInterval;
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            if ( this._flashingClock - this._clockStartTime > this._clockInterval ) {
                this._flashingClock = Time.time;
                this._clockStartTime = this._flashingClock;

                this.ClockTicked();

            }

            this._flashingClock += Time.deltaTime;
        }

        void OnMouseEnter() {
            this._clockInterval = this._fastFlashInterval;
        }

        void OnMouseExit() {
            this._clockInterval = this._slowFlashInterval;
        }

        private void ClockTicked() {
            this._ren.Background = this._ren.Background == Color.black ? Color.red : Color.black;
        }

        #endregion


        #region Fields

        private ItemRendering _ren;
        private float _clockInterval;
        private readonly float _slowFlashInterval = 0.25f;
        private readonly float _fastFlashInterval = 0.1f;
        private float _flashingClock;
        private float _clockStartTime;

        #endregion

    }
}
