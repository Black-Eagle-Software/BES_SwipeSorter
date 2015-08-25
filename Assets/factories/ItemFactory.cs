using System;
using System.Collections.Generic;
using Assets.managers;
using Assets.models;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.factories {
    public class ItemFactory : Singleton<ItemFactory> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            //get viewport extents
            var cam = Camera.main;
            this.xMin = cam.ViewportToWorldPoint( new Vector2( cam.rect.xMin, 0f ) ).x + 10f;  //offset away from boundaries
            this.xMax = cam.ViewportToWorldPoint( new Vector2( cam.rect.xMax, 0f ) ).x - 10f;  //offset away from boundaries
            this.yMin = cam.ViewportToWorldPoint( new Vector2( 0, cam.rect.yMin ) ).y - 5f;   //offset down so items disappear offscreen
            this.yMax = cam.ViewportToWorldPoint( new Vector2( 0, cam.rect.yMax ) ).y;
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public GameObject SpawnBomb() {
            var x = Random.Range( this.xMin, this.xMax );
            var b = ( GameObject )Instantiate( this.BombObject, new Vector2( x, this.yMax ), Quaternion.identity );
            b.GetComponent<ItemBase>().Type = ItemType.BOMB;

            b.AddComponent<ItemPosition>();
            var ip = b.GetComponent<ItemPosition>();
            ip.yMin = this.yMin;
            return b;
        }
        public GameObject SpawnBlocker() {
            var x = Random.Range( this.xMin, this.xMax );
            var b = ( GameObject )Instantiate( this.BlockerObject, new Vector2( x, this.yMax ), Quaternion.identity );
            var ib = b.GetComponent<ItemBase>();
            ib.Type = ItemType.BLOCKER;

            b.AddComponent<ItemRendering>();
            var ir = b.GetComponent<ItemRendering>();
            var clr = this.TokenColors[Random.Range( 0, this.TokenColors.Count )];
            ir.Background = clr.Value;
            b.tag = clr.Tag;

            b.AddComponent<ItemPosition>();
            var ip = b.GetComponent<ItemPosition>();
            ip.yMin = this.yMin;
            return b;
        }
        public GameObject SpawnBoundary( Vector2 position ) {
            var b = ( GameObject )Instantiate( this.BoundaryObject, position, Quaternion.identity );
            var ib = b.GetComponent<ItemBase>();
            ib.Type = ItemType.BOUNDARY;
            return b;
        }
        public GameObject SpawnToken() {
            var indx = Random.Range( 0, this.TokensList.Count );
            var x = Random.Range( this.xMin, this.xMax );
            var c = ( GameObject )Instantiate( this.TokensList[indx], new Vector2( x, this.yMax ), Quaternion.identity );
            var ib = c.GetComponent<ItemBase>();
            ib.Type = ItemType.TOKEN;

            c.AddComponent<ItemRendering>();
            var ir = c.GetComponent<ItemRendering>();
            var clr = this.TokenColors[Random.Range( 0, this.TokenColors.Count )];
            ir.Background = clr.Value;
            c.tag = clr.Tag;

            c.AddComponent<ItemPosition>();
            var ip = c.GetComponent<ItemPosition>();
            ip.yMin = this.yMin;

            //token specific scripts
            c.AddComponent<TokenInput>();
            c.AddComponent<TokeSelection>();
            c.AddComponent<TokenCollision>();
            return c;
        }

        #endregion


        #region Fields

        public GameObject BlockerObject;
        public GameObject BombObject;
        public GameObject BoundaryObject;
        public List<GameObject> TokensList;
        public List<ItemColor> TokenColors;

        private float xMin;
        private float xMax;
        private float yMax;
        private float yMin;

        #endregion
    }

    [Serializable]
    public class ItemColor {
        public string Tag;
        public Color Value;
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer( typeof( ItemColor ) )]
    public class TokenEntryDrawer : PropertyDrawer {
        public override void OnGUI( Rect position, SerializedProperty property, GUIContent label ) {
            //base.OnGUI( position, property, label );
            EditorGUI.BeginProperty( position, label, property );
            //position = EditorGUI.PrefixLabel( position, GUIUtility.GetControlID( FocusType.Passive ), label );
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 1;

            var tagRect = new Rect( position.x, position.y, position.width * 0.5f, position.height );
            var tokenRect = new Rect( position.width * 0.52f, position.y, position.width * 0.53f, position.height );

            EditorGUI.PropertyField( tagRect, property.FindPropertyRelative( "Tag" ), GUIContent.none );
            EditorGUI.PropertyField( tokenRect, property.FindPropertyRelative( "Value" ), GUIContent.none );

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
#endif
}
