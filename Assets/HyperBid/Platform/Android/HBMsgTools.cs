using UnityEngine;
using System.Collections;


#if UNITY_ANDROID

public class HBMsgTools
{
	private  AndroidJavaObject _Plugin;

	public HBMsgTools ()
	{
		try{
			if (Application.platform != RuntimePlatform.Android)
				return;

			_Plugin = new AndroidJavaObject ("com.hyperbid.unitybridge.MsgTools");

		}catch(System.Exception e)
		{
			System.Console.WriteLine("Exception caught: {0}", e);
		}
	}


	public  void printLogI (string msg)
	{
		try{
			
			_Plugin.Call ("printLogI",msg);
		}catch(System.Exception e)
		{
			System.Console.WriteLine("Exception caught: {0}", e);
		}
	}


	public  void printMsg (string msg)
	{
		try{
			_Plugin.Call ("pirntMsg",msg);
		}catch(System.Exception e)
		{
			System.Console.WriteLine("Exception caught: {0}", e);
		}
	}


}

#endif



