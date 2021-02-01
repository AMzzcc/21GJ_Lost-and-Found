using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Masker : MonoBehaviour {

	public Shader maskShader;

	private Material maskMaterial;

	protected void Start() {
		maskMaterial = CheckShaderAndCreateMaterial(maskShader, maskMaterial);
	}

	// Called when need to create the material used by this effect
	protected Material CheckShaderAndCreateMaterial(Shader shader, Material material) {
		if (shader == null) {
			return null;
		}
		if (!shader.isSupported) {
			return null;
		} else if (material && material.shader == shader) {
			return material;
		} else {
			material = new Material(shader);
			material.hideFlags = HideFlags.DontSave;
			return material;
		}
	}

	public Transform focusedObject;

	public float visibleRadius = 1.0f; // 可视半径
	[Range(-30.0f, 120.0f)]
	public float visibleAngle = 30.0f; // 可视角度
	public float visibleDistance = 50.0f;
	public float gradientLength = 1.0f; // 边界模糊
	[Range(0, 60.0f)]
	public float gradientAngle = 5.0f;

	public Color maskColor;
	public Texture2D maskTexture;

	private void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if (maskMaterial) {
			var mousePos = Input.mousePosition;
			var cameraPos = Camera.main.transform.position;
			
			maskMaterial.SetColor("_MaskColor", maskColor);
			if (maskTexture) {
				maskMaterial.SetTexture("_MaskTexture", maskTexture);
			}
			maskMaterial.SetFloat("_VisibleRadius", visibleRadius);
			maskMaterial.SetFloat("_VisibleAngle", visibleAngle);
			maskMaterial.SetFloat("_VisibleDistance", visibleDistance);
			maskMaterial.SetFloat("_GradientLength", gradientLength);
			maskMaterial.SetFloat("_GradientAngle", gradientAngle);
			maskMaterial.SetVector("_MousePosition", mousePos);
			maskMaterial.SetVector("_CameraPosition", cameraPos);
			var focusedPosInScrSpace = TransformationMatrixUtil.PToScreenPosition(TransformationMatrixUtil.VToPPosition(TransformationMatrixUtil.WToVPosition(focusedObject.position)));
			var viewDir = new Vector4(mousePos.x, mousePos.y, mousePos.z, 1.0f) - focusedPosInScrSpace;
			maskMaterial.SetVector("_ViewDirection", viewDir);
			maskMaterial.SetVector("_FocusedPosition", focusedPosInScrSpace);

			maskMaterial.SetFloat("_ScreenWidth", Screen.width);
			maskMaterial.SetFloat("_ScreenHeight", Screen.height);

			Graphics.Blit(source, destination, maskMaterial);
		} else {
			Graphics.Blit(source, destination);
		}
	}
}
