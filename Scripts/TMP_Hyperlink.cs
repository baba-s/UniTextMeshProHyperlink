#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniTextMeshProHyperlink
{
	[RequireComponent( typeof( TextMeshProUGUI ) )]
	public class TMP_Hyperlink : MonoBehaviour, IPointerClickHandler
	{
		public event Action<string> OnClick;

		public void OnPointerClick( PointerEventData e )
		{
			var text   = GetComponent<TextMeshProUGUI>();
			var pos    = Input.mousePosition;
			var camera = text.canvas.worldCamera;
			var index  = TMP_TextUtilities.FindIntersectingLink( text, pos, camera );

			if ( index == -1 ) return;
			
			var linkInfo = text.textInfo.linkInfo[ index ];
			var url      = linkInfo.GetLinkID();

			if ( OnClick != null )
			{
				OnClick( url );
				return;
			}

#if !UNITY_EDITOR && UNITY_WEBGL
			OpenToBlankWindow( url );
#else
			Application.OpenURL( url );
#endif
		}

		public static void OpenURLInWebGL( string url )
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			OpenToBlankWindow( url );
#endif
		}

#if !UNITY_EDITOR && UNITY_WEBGL
		[DllImport( "__Internal" )]
		private static extern void OpenToBlankWindow( string _url );
#endif
	}
}