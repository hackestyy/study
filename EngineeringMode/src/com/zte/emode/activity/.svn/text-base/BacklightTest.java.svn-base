package com.zte.emode.activity;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.os.Handler;
import android.os.IPowerManager;
import android.os.Message;
import android.os.RemoteException;
import android.os.ServiceManager;
import android.provider.Settings;
import android.provider.Settings.SettingNotFoundException;
import android.util.Log;
import android.widget.TextView;

import com.zte.emode.R;

@SuppressLint("HandlerLeak")
public class BacklightTest extends EmodeActivity {
	private static final String TAG = "Emode";
	private TextView title;
	private int oldBrightness;
	private boolean stopFlag = false;

	private static final int BRIGHT = 250;
	private static final int DARK = 20;
	private static final int STOP = 3;

	private IPowerManager powerManager;

	public Handler handler = new Handler() {
		public void handleMessage(Message msg) {
			switch (msg.what) {
			case BRIGHT:
			case DARK:
				try {
					if (!stopFlag)
						powerManager.setBacklightBrightness(msg.what);
				} catch (RemoteException e) {
				}
				sendEmptyMessageDelayed(msg.what == DARK ? BRIGHT : DARK, 1000);
				break;

			case STOP:
				removeMessages(DARK);
				removeMessages(BRIGHT);
				try {
					powerManager.setBacklightBrightness(oldBrightness);
				} catch (RemoteException e) {
				}
				break;
			default:
				break;
			}
		}
	};

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.backlighttest);

		title = (TextView) findViewById(R.id.bl_title);
		title.setText("Backlight Test");

		try {
			oldBrightness = Settings.System.getInt(getContentResolver(),
					Settings.System.SCREEN_BRIGHTNESS);
		} catch (SettingNotFoundException snfe) {
			oldBrightness = 100;
		}
		
		powerManager = IPowerManager.Stub.asInterface(ServiceManager
				.getService("power"));

		handler.sendEmptyMessage(DARK);
	}

	@Override
	protected void onResume() {
		super.onResume();
		Log.d(TAG, "onResume");
		stopFlag = false;
	}

	@Override
	protected void onPause() {
		super.onPause();
		Log.d(TAG, "onPause");

		stopFlag = true;
		try {
			powerManager.setBacklightBrightness(oldBrightness);
		} catch (RemoteException e) {
			Log.e(TAG, e.getMessage());
		}
	}

	@Override
	protected void onStop() {
		super.onStop();
		handler.sendEmptyMessage(STOP);
	}
}
