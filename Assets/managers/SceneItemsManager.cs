using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.factories;
using Assets.models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.managers {
    public class SceneItemsManager : Singleton<SceneItemsManager> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._itemFactory = ItemFactory.Instance;
            this._factoryClock = Time.time;
            this._clockStartTime = this._factoryClock;

            //find the main camera and get the viewport bounds
            var cam = Camera.main;
            var b1Pos = cam.ViewportToWorldPoint( new Vector2( cam.rect.xMin, 0.5f ) );
            var b2Pos = cam.ViewportToWorldPoint( new Vector2( cam.rect.xMax, 0.5f ) );
            this.AddBoundariesToScreenEdges( b1Pos, b2Pos );
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            if ( this._factoryClock - this._clockStartTime > this._clockInterval ) {
                this._factoryClock = Time.time;
                this._clockStartTime = this._factoryClock;

                this.ClockTicked( this.ShouldSpawnItems );

            }

            this.UpdateSceneItems();

            this._factoryClock += Time.deltaTime;
        }

        public void AddBoundariesToScreenEdges( Vector2 firstPosition, Vector2 secondPosition ) {
            //spawn a boundary on left and right edges of the screen
            var b1 = this._itemFactory.SpawnBoundary( firstPosition );
            this._itemsToAdd.Add( b1 );
            var b2 = this._itemFactory.SpawnBoundary( secondPosition );
            var b1Ir = b1.GetComponent<ItemRendering>();
            var b2Ir = b2.GetComponent<ItemRendering>();
            while ( b2Ir.Background == b1Ir.Background ) {
                this.ChangeBoundaryColor( b2 );
            }
            this._itemsToAdd.Add( b2 );
            this.LeftTokenColor = b1Ir.Background;
            this.RightTokenColor = b2Ir.Background;
        }

        private void ChangeBoundaryColor( GameObject boundary ) {
            var ir = boundary.GetComponent<ItemRendering>();
            ir.Background = this._itemFactory.TokenColors[Random.Range( 0, this._itemFactory.TokenColors.Count )].Value;
        }

        private void ClockTicked( bool shouldSpawn ) {
            if ( !shouldSpawn ) return;
            var sType = Random.Range( 0f, 100f );
            //Debug.Log( sType );
            if ( sType <= 20f && sType > 10f && this._spawnedItems.Count < this.MaxItemsOnScreen ) {
                //spawn a new blocker
                var blkr = this._itemFactory.SpawnBlocker();
                blkr.GetComponent<ItemPosition>().ItemIsOffScreen += this.token_IsOffScreen;
                foreach ( var st in this._spawnedTokens.Where( st => st.tag == blkr.tag ) ) {
                    Physics2D.IgnoreCollision( blkr.gameObject.GetComponent<Collider2D>(),
                        st.gameObject.GetComponent<Collider2D>(), true );
                }
                this._itemsToAdd.Add( blkr );
            } else if ( sType > 5f && sType <= 10f ) {
                var bomb = this._itemFactory.SpawnBomb();
                bomb.GetComponent<ItemPosition>().ItemIsOffScreen += this.token_IsOffScreen;
                this._itemsToAdd.Add( bomb );
            } else {
                if ( this._spawnedTokens.Count < this.MaxTokensOnScreen ) {
                    //spawn a new token
                    var c = this._itemFactory.SpawnToken();
                    c.GetComponent<ItemPosition>().ItemIsOffScreen += this.token_IsOffScreen;
                    c.GetComponent<TokenCollision>().HitBoundary += this.token_HitBoundary;
                    this._itemsToAdd.Add( c );
                }
            }
        }

        void token_HitBoundary( GameObject o ) {
            this._itemsToRemove.Add( o );
            this.OnScoreIncreased();
        }

        private void token_IsOffScreen( GameObject o ) {
            if ( o.GetComponent<ItemBase>().Type == ItemType.TOKEN ) {
                var bg = o.GetComponent<ItemRendering>().Background;
                if ( bg == this.LeftTokenColor || bg == this.RightTokenColor ) {
                    //let a target token fall off the screen
                    this.OnScoreDecreased();
                }
            }
            this._itemsToRemove.Add( o );
        }

        private void UpdateSceneItems() {
            foreach ( var i in this._itemsToRemove ) {
                var t = i.GetComponent<ItemBase>().Type;
                switch ( t ) {
                    case ItemType.BLOCKER:
                        this._spawnedItems.Remove( i );
                        break;
                    case ItemType.BOMB:
                        this._spawnedItems.Remove( i );
                        break;
                    case ItemType.BOUNDARY:
                        this._spawnedBoundaries.Remove( i );
                        break;
                    case ItemType.TOKEN:
                        this._spawnedTokens.Remove( i );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                Destroy( i );
            }

            foreach ( var i in this._itemsToAdd ) {
                var t = i.GetComponent<ItemBase>().Type;
                switch ( t ) {
                    case ItemType.BLOCKER:
                        this._spawnedItems.Add( i );
                        break;
                    case ItemType.BOMB:
                        this._spawnedItems.Add( i );
                        break;
                    case ItemType.BOUNDARY:
                        this._spawnedBoundaries.Add( i );
                        break;
                    case ItemType.TOKEN:
                        this._spawnedTokens.Add( i );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            this._itemsToAdd.Clear();
            this._itemsToRemove.Clear();
        }

        protected virtual void OnScoreIncreased() {
            var handler = this.ScoreIncreased;
            if ( handler != null ) handler();
        }
        protected virtual void OnScoreDecreased() {
            var handler = this.ScoreDecreased;
            if ( handler != null ) handler();
        }

        #endregion


        #region Fields

        public bool ShouldSpawnItems = false;
        public int MaxItemsOnScreen = 5;
        public int MaxTokensOnScreen = 50;
        public Color LeftTokenColor;
        public Color RightTokenColor;

        public delegate void ScorePlus();
        public event ScorePlus ScoreIncreased;
        public delegate void ScoreMinus();
        public event ScoreMinus ScoreDecreased;

        private ItemFactory _itemFactory;

        private readonly List<GameObject> _spawnedBoundaries = new List<GameObject>();
        private readonly List<GameObject> _spawnedTokens = new List<GameObject>();
        private readonly List<GameObject> _spawnedItems = new List<GameObject>();

        private readonly List<GameObject> _itemsToAdd = new List<GameObject>();
        private readonly List<GameObject> _itemsToRemove = new List<GameObject>();
        private readonly float _clockInterval = 0.7f;
        private float _factoryClock;
        private float _clockStartTime;

        #endregion


    }
}
