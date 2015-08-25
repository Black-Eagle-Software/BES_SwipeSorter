using System.Collections.Generic;
using Assets.factories;
using Assets.models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.managers {
    public class GameManager : Singleton<GameManager> {
        #region Constructors



        #endregion


        #region Properties

        public bool GameIsRunning {
            get { return this._gameIsRunning; }
            set {
                this._gameIsRunning = value;
                Debug.Log( string.Format( "Game is running? {0}", value ) );
                if ( value ) {
                    this.StartGame();
                } else {
                    this.EndGame();
                }
            }
        }

        public int Score {
            get { return this._score; }
            protected set {
                if ( value < 0 ) {
                    //just lost
                    Time.timeScale = 0;
                    this._gameIsOver = true;
                    this.GameIsRunning = false;
                    this._score = 0;
                } else {
                    this._score = value;
                }
                this.ScoreTextField.text = this.Score.ToString();
            }
        }

        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._sceneMgr = SceneItemsManager.Instance;
            this._sceneMgr.ScoreIncreased += this._sceneMgr_ScoreIncreased;
            this._sceneMgr.ScoreDecreased += this._sceneMgr_ScoreDecreased;
        }

        void _sceneMgr_ScoreDecreased() {
            this.Score--;
        }

        void _sceneMgr_ScoreIncreased() {
            this.Score++;
        }

        // Update is called once per frame
        void Update() {
            if ( !this.GameIsRunning && !this._gameIsOver ) {    //eventually want this controlled by a button/event of some sort
                this.GameIsRunning = true;
            }
        }

        private void StartGame() {
            this.Score = 0;
            this._sceneMgr.ShouldSpawnItems = true;
        }

        private void EndGame() {
            this._sceneMgr.ShouldSpawnItems = false;
        }
        
        #endregion


        #region Fields

        public Text ScoreTextField;

        private SceneItemsManager _sceneMgr;
        private bool _gameIsRunning;
        private bool _gameIsOver;
        private int _score;

        #endregion

    }
}
