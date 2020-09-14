using UnityEngine;
using System.Collections;

namespace DevionGames.UIWidgets{
	[System.Serializable]
	/// <summary>
	/// Message options.
	/// </summary>
	public class NotificationOptions {
		/// <summary>
		/// The title to display.
		/// </summary>
		public string title = string.Empty;
		/// <summary>
		/// The message to display.
		/// </summary>
		public string text = string.Empty;
		/// <summary>
		/// The color of the text.
		/// </summary>
		public Color color = Color.white;
		/// <summary>
		/// The icon to display.
		/// </summary>
		public Sprite icon;
		/// <summary>
		/// The delay before fading
		/// </summary>
		public float delay = 10.0f;
		/// <summary>
		/// The duration of fading.
		/// </summary>
		public float duration = 5.0f;
		/// <summary>
		/// Ignore TimeScale.
		/// </summary>
		public bool ignoreTimeScale = true;

		public NotificationOptions(NotificationOptions other){
			title = other.title;
			text  = other.text;
			icon  = other.icon;
			color = other.color;
			duration = other.duration;
			ignoreTimeScale = other.ignoreTimeScale;
		}
		
		public NotificationOptions(){}
	}
}