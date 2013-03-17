package com.zte.emode;

import java.io.IOException;
import java.security.MessageDigest;
import java.util.Locale;
import java.util.Properties;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class ActivityLauncher {
	private static final String TAG = "EMODE";
	private static ActivityLauncher activityLauncher = null;
	private static final String ACTIVITY_PACKAGE = "pkg";
	private Properties pro = null;
	
	private ActivityLauncher(){
	}
	public static ActivityLauncher getInstance(){
		if (null == activityLauncher) {
			activityLauncher = new ActivityLauncher();			
		}
		return activityLauncher;
	}
	public void startActivityByKey(Context context,String key){
		Intent intent = new Intent();
		Log.v(TAG, "startActivityByKey");
		initProperties(context);
		intent.setComponent(new ComponentName(pro.getProperty(getMD5Str(key)+ACTIVITY_PACKAGE), pro.getProperty(getMD5Str(key))));
		intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		context.startActivity(intent);
	}
	private void initProperties(Context context) {
		try {
			if (null==pro) {
				pro = new Properties();
				pro.load(context.getAssets().open("EmodeProperties.properties"));
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	private String getMD5Str(String str) {
		MessageDigest messageDigest = null;

		try {
			messageDigest = MessageDigest.getInstance("MD5");
			messageDigest.reset();
			messageDigest.update(str.getBytes("UTF-8"));
		} catch (Exception e) {
			return null;
		}

		byte[] byteArray = messageDigest.digest();

		StringBuffer md5StrBuff = new StringBuffer();

		for (int i = 0; i < byteArray.length; i++) {
			if (Integer.toHexString(0xFF & byteArray[i]).length() == 1)
				md5StrBuff.append("0").append(
						Integer.toHexString(0xFF & byteArray[i]));
			else
				md5StrBuff.append(Integer.toHexString(0xFF & byteArray[i]));
		}
		// 16 encryption, from 9th bit to 25th bit
		return md5StrBuff.substring(8, 24).toString()
				.toUpperCase(Locale.ENGLISH);
	}
}
