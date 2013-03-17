package com.zte.emode;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class EmodeReceiver extends BroadcastReceiver {
	private static final String TAG = "EMODE";
	private static final String EMODE_KEY = "com.zte.smartdialer.input";
	@Override
	public void onReceive(Context context, Intent intent) {
		Log.i(TAG, "onReceive");
		String keyString = intent.getStringExtra(EMODE_KEY);
		if (null!=keyString) {
			Log.v(TAG, keyString);
			ActivityLauncher.getInstance().startActivityByKey(context,keyString);
		}
	}

}
