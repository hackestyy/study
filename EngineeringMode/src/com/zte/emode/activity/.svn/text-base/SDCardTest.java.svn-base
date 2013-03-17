package com.zte.emode.activity;

import android.content.Context;
import android.os.Bundle;
import android.os.Environment;
import android.os.StatFs;
import android.os.storage.StorageManager;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.zte.emode.R;

public class SDCardTest extends EmodeActivity {
	private static final String TAG = "Emode";

	private TextView titleText;
	private TextView internalTitleText;
	private TextView internalStatusText;
	private TextView internalUsedText;
	private TextView internalTotalText;
	private TextView internalAvailableText;
	private TextView externalTitleText;
	private TextView externalStatusText;
	private TextView externalUsedText;
	private TextView externalTotalText;
	private TextView externalAvailableText;
	private Button backButton;

	private String internalStatusContent;
	private String internalTotalContent;
	private String internalAvailableContent;
	private String internalUsedContent;
	private String externalStatusContent;
	private String externalTotalContent;
	private String externalAvailableContent;
	private String externalUsedContent;

	private StatFs internalStatFs;
	private StatFs externalStatFs;
	private static long internalBlockSize = 0;
	private static long internalTotalBlocks = 0;
	private static long internalAvailableBlocks = 0;
	private static long externalBlockSize = 0;
	private static long externalTotalBlocks = 0;
	private static long externalAvailableBlocks = 0;

	private static StorageManager storageManager = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.sdcardtest);
		Log.v(TAG, "Sdcardtest");
		if (storageManager == null) {
			storageManager = (StorageManager) getSystemService(Context.STORAGE_SERVICE);
		}

		titleText = (TextView) findViewById(R.id.sd_title);
		titleText.setText("SD Test");

		internalTitleText = (TextView) findViewById(R.id.internal_sd_title);
		internalTitleText.setText("Internal Storage");

		internalStatusText = (TextView) findViewById(R.id.internal_sd_status);
		internalUsedText = (TextView) findViewById(R.id.internal_sd_used);
		internalTotalText = (TextView) findViewById(R.id.internal_sd_total);
		internalAvailableText = (TextView) findViewById(R.id.internal_sd_available);

		externalTitleText = (TextView) findViewById(R.id.external_sd_title);
		externalTitleText.setText("External Storage");

		externalStatusText = (TextView) findViewById(R.id.external_sd_status);
		externalUsedText = (TextView) findViewById(R.id.external_sd_used);
		externalTotalText = (TextView) findViewById(R.id.external_sd_total);
		externalAvailableText = (TextView) findViewById(R.id.external_sd_available);

		updateMemoryStatus();

		backButton = (Button) findViewById(R.id.sd_b);
		backButton.setOnClickListener(new Button.OnClickListener() {
			public void onClick(View v) {
				finish();
			}
		});
		backButton.setText("OK");
	}

	private void updateMemoryStatus() {
		internalBlockSize = 0;
		internalTotalBlocks = 0;
		internalAvailableBlocks = 0;
		externalBlockSize = 0;
		externalTotalBlocks = 0;
		externalAvailableBlocks = 0;
		
		Log.i(TAG, "Internal Storage Path " + storageManager.getUsbStorageVolume().getPath());
		getInternalStorageInfo(storageManager.getUsbStorageVolume().getPath());
		
		Log.i(TAG, "External Storage Path " + storageManager.getSdCardVolume().getPath());
		getExternalStroageInfo(storageManager.getSdCardVolume().getPath());

		internalTotalContent = formatSize(internalTotalBlocks
				* internalBlockSize);
		internalAvailableContent = formatSize(internalAvailableBlocks
				* internalBlockSize);
		internalUsedContent = formatSize((internalTotalBlocks - internalAvailableBlocks)
				* internalBlockSize);
		externalTotalContent = formatSize(externalTotalBlocks
				* externalBlockSize);
		externalAvailableContent = formatSize(externalAvailableBlocks
				* externalBlockSize);
		externalUsedContent = formatSize((externalTotalBlocks - externalAvailableBlocks)
				* externalBlockSize);

		internalStatusText.setText("Status:" + internalStatusContent);
		internalUsedText.setText("Used:" + internalUsedContent);
		internalTotalText.setText("Total:" + internalTotalContent);
		internalAvailableText.setText("Available:" + internalAvailableContent);

		externalStatusText.setText("Status:" + externalStatusContent);
		externalUsedText.setText("Used:" + externalUsedContent);
		externalTotalText.setText("Total:" + externalTotalContent);
		externalAvailableText.setText("Available:" + externalAvailableContent);
	}

	private void getInternalStorageInfo(String internalStoragePath) {
		internalStatusContent = storageManager
				.getVolumeState(internalStoragePath);
		if (Environment.MEDIA_MOUNTED.equals(internalStatusContent)
				|| Environment.MEDIA_MOUNTED_READ_ONLY
						.equals(internalStatusContent)) {
			try {
				if (internalStatFs == null) {
					internalStatFs = new StatFs(internalStoragePath);
				}
				internalBlockSize = internalStatFs.getBlockSize();
				internalTotalBlocks = internalStatFs.getBlockCount();
				internalAvailableBlocks = internalStatFs.getAvailableBlocks();

			} catch (IllegalArgumentException e) {
				// do nothing
			}
		}
	}

	private void getExternalStroageInfo(String externalStoragePath) {

		externalStatusContent = storageManager
				.getVolumeState(externalStoragePath);
		if (Environment.MEDIA_MOUNTED.equals(externalStatusContent)
				|| Environment.MEDIA_MOUNTED_READ_ONLY
						.equals(externalStatusContent)) {
			try {
				if (externalStatFs == null) {
					externalStatFs = new StatFs(externalStoragePath);
				}
				externalBlockSize = externalStatFs.getBlockSize();
				externalTotalBlocks = externalStatFs.getBlockCount();
				externalAvailableBlocks = externalStatFs.getAvailableBlocks();

			} catch (IllegalArgumentException e) {
				// do nothing
			}
		}
	}

	private String formatSize(long size) {

		String suffix = null;

		if (size <= 0)
			return "0";

		// add K or M suffix if size is greater than 1K or 1M
		if (size >= 1024) {
			suffix = "K";
			size /= 1024;
			if (size >= 1024) {
				suffix = "M";
				size /= 1024;
			}
		}

		StringBuilder resultBuffer = new StringBuilder(Long.toString(size));

		int commaOffset = resultBuffer.length() - 3;
		while (commaOffset > 0) {
			resultBuffer.insert(commaOffset, ',');
			commaOffset -= 3;
		}

		if (suffix != null)
			resultBuffer.append(suffix);

		return resultBuffer.toString();
	}
}
