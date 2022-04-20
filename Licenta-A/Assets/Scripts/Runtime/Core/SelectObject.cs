using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AF
{
	public class SelectObject : MonoBehaviour
	{
		private Renderer objectRenderer;
		private Material material;
		private Texture transparentTtexture;

		private void Awake()
		{
			objectRenderer = GetComponent<Renderer>();
			material = objectRenderer.material;
			transparentTtexture = Resources.Load<Texture>("Textures/transparent");
		}

		public void Select()
		{
			objectRenderer.material = new Material(material);
		}

		public void Deselect()
		{
			var newMaterial = new Material(material);
			newMaterial.SetTexture("_Texture", transparentTtexture);
			newMaterial.SetTexture("_Texture_Triplanar", transparentTtexture);
			objectRenderer.material = newMaterial;
		}
	}
}
