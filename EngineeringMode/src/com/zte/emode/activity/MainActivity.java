package com.zte.emode.activity;

import android.content.Intent;
import android.os.Bundle;

public class MainActivity extends EmodeActivity {
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent intent = new Intent();
        intent.setAction("yytest");
        sendBroadcast(intent);
	}
}
