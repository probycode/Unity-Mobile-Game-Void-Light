using UnityEngine;
using System.Collections;
using System;

public class Fading : MonoBehaviour {

    public delegate void FadingEventHandler();

    public static event FadingEventHandler FadingDone;

    public static Fading Instance;

	public Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic
	public float fadeSpeed = 0.8f;		// the fading speed

	private int drawDepth = -1000;		// the texture's order in the draw hierarchy: a low number means it renders on top
	private float alpha = 1.0f;			// the texture's alpha value between 0 and 1
	private int fadeDir = -1;           // the direction to fade: in = -1 or out = 1
    private Action OnCompletion;

    public static void OnFadingDone()
    {
        if (FadingDone != null)
        {
            FadingDone();
        }
    }

    void Awake ()
	{
		Instance = this;
        ViewController.ViewChanged += ViewController_ViewChanged;
        FadingDone += Fading_FadingDone;
        if(fadeOutTexture == null)
        {
            CreateTexure(Color.black);
        }
    }

    private void Fading_FadingDone()
    {
        BeginFade(-1, 0.8f);
    }

    private void OnDestroy()
    {
        ViewController.ViewChanged -= ViewController_ViewChanged;
    }

    private void ViewController_ViewChanged(View view)
    {
        CreateTexure(Color.black);
        BeginFade(1, 100f);
    }

    void CreateTexure (Color color )
    {
        fadeOutTexture = new Texture2D(1,1);
        for (int x = 0; x < fadeOutTexture.width; x++)
        {
            for (int y = 0; y < fadeOutTexture.height; y++)
            {
                fadeOutTexture.SetPixel(x,y, color);
            }
        }
        fadeOutTexture.Apply();
    }

	void OnGUI()
	{
		// fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		// set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;																// make the black texture render on top (drawn last)
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);       // draw the texture to fit the entire screen area
        if (GUI.color.a >= 0.95f)
        {
            if (OnCompletion != null)
            {
                OnCompletion();
                OnCompletion = null;
            }
            OnFadingDone();
        }
    }

    // sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int direction, float fadeSpeed)
    {
        this.fadeSpeed = fadeSpeed;
        fadeDir = direction;
        return (fadeSpeed);
    }

    public float BeginFade(int direction, float fadeSpeed, Color fadeColor,  Action OnCompletion)
    {
        CreateTexure(fadeColor);
        this.fadeSpeed = fadeSpeed;
        fadeDir = direction;
        this.OnCompletion = OnCompletion;
        return (fadeSpeed);
    }

    // OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
    /*void OnLevelWasLoaded()
	{
		// alpha = 1;		// use this if the alpha is not set to 1 by default
		BeginFade(-1, fadeSpeed);		// call the fade in function
	}*/
}
