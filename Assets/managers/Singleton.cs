using UnityEngine;

namespace Assets.managers {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        protected static T _instance;

        public static T Instance {
            get {
                if ( _instance != null ) return _instance;
                _instance = ( T )FindObjectOfType( typeof( T ) );
                if ( _instance == null ) {
                    Debug.LogError( "An instance of " + typeof( T ) + " is needed in the scene, but there is none." );
                }
                return _instance;
            }
        }
    }
}
