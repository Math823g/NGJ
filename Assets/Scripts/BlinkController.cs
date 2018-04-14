﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BlinkController : MonoBehaviour {

	public delegate void BlinkDelegate();
	public static event BlinkDelegate OnBlink;
	public static event BlinkDelegate OnBlinkEnd;
    public Image crosshair;

	private Animator anim;

	private Material blinkMat;

	public static BlinkController Singleton;

	[Range(0,1)]
	public float Progress;


	public void Update(){
		if (Input.GetButtonDown ("Blink")) {
			InitiateBlink ();
		}
	}

	public void Start(){
		blinkMat = new Material(Shader.Find("Custom/BlinkShader"));

		anim = GetComponent<Animator> ();

		OnBlink += RandomBlinkStuff;
		OnBlinkEnd += RandomBLinkStuffEnd;

		Singleton = this;
        crosshair = GameObject.Find("CrossHair").GetComponent<Image>();
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst){
		blinkMat.SetFloat ("_Progress", Progress);
		Graphics.Blit (src, dst, blinkMat);
	}

	public static void RandomBlinkStuff(){
		print ("BlinkStart");
    }

	public static void RandomBLinkStuffEnd(){
		print ("BlinkEnd");
	}

	public void InitiateBlink(){
		anim.SetTrigger ("Blink");
	}

	private static void InitiateBlinkRender(){
		
	}

	/*
	public void OnGUI(){
		if (GUI.Button (new Rect(new Vector2(100,100), new Vector2(100,20)), "BLINK")) {
			InitiateBlink ();
		}
	}
	*/
	public void StartBlink (){
		if (OnBlink != null) {
			OnBlink ();
            crosshair.enabled = false;
        }
	}

	public void StopBlink(){
		if (OnBlinkEnd != null) {
			OnBlinkEnd();
            crosshair.enabled = true;
        }
	}

	public IEnumerator BlackEffectRoutine(){
		yield return new WaitForEndOfFrame ();

	}

}
