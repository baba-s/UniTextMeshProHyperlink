#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KoganeUnityLib
{
	[RequireComponent( typeof( TextMeshProUGUI ) )]
	public class TMP_Hyperlink : MonoBehaviour, IPointerClickHandler
	{
		public Action<string> mOnClick;

		public void OnPointerClick( PointerEventData e )
		{
			Debug.Log( "aaa" );
			var text   = GetComponent<TextMeshProUGUI>();
			var pos    = Input.mousePosition;
			var camera = text.canvas.worldCamera;
			var index  = TMP_TextUtilities.FindIntersectingLink( text, pos, camera );

			if ( index == -1 ) return;

			var linkInfo = text.textInfo.linkInfo[ index ];
			var url      = linkInfo.GetLinkID();

			Debug.Log( "aaa" );
			mOnClick?.Invoke( url );

#if !UNITY_EDITOR && UNITY_WEBGL
			OpenToBlankWindow( url );
#else
			Application.OpenURL( url );
#endif
		}

#if !UNITY_EDITOR && UNITY_WEBGL
		[DllImport( "__Internal" )]
		private static extern void OpenToBlankWindow( string _url );
#endif
	}
}