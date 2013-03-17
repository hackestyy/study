package com.example.unitconversion.test;

import junit.framework.TestCase;
import android.test.suitebuilder.annotation.SmallTest;

import com.example.unitconversion.Length;
import com.example.unitconversion.LengthUnit;
import com.example.unitconversion.StaticVar;

public class TestUnitConversion extends TestCase {

	private LengthUnit mile;
	private LengthUnit yard;
	private LengthUnit feet;
	private LengthUnit inch;

	public TestUnitConversion(String name) {
		super(name);
		mile = new LengthUnit(StaticVar.FEET2INCH * StaticVar.INCH2BASE
				* StaticVar.MILE2YARD * StaticVar.YARD2FEET, StaticVar.MILE);
		yard = new LengthUnit(StaticVar.FEET2INCH * StaticVar.INCH2BASE
				* StaticVar.YARD2FEET, StaticVar.YARD);
		feet = new LengthUnit(StaticVar.FEET2INCH * StaticVar.INCH2BASE,
				StaticVar.FEET);
		inch = new LengthUnit(StaticVar.INCH2BASE, StaticVar.INCH);
	}

	@SmallTest
	public void testMile2Yard() {
		Length mile1 = new Length(1, mile);
		Length yard1000 = new Length(1000, yard);
		Length yard1760 = new Length(1760, yard);
		assertTrue(mile1.equals(yard1760));
		assertFalse(mile1.equals(yard1000));
	}

	@SmallTest
	public void testYard2Feet() {
		Length yard1 = new Length(1, yard);
		Length feet3 = new Length(3, feet);
		Length feet2 = new Length(2, feet);
		assertTrue(yard1.equals(feet3));
		assertFalse(yard1.equals(feet2));
	}

	@SmallTest
	public void testFeet2Inch() {
		Length feet1 = new Length(1, feet);
		Length inch12 = new Length(12, inch);
		Length inch11 = new Length(11, inch);
		assertTrue(feet1.equals(inch12));
		assertFalse(feet1.equals(inch11));
	}

	@SmallTest
	public void testMile2Inch() {
		Length mile1 = new Length(1, mile);
		Length inch12 = new Length(12, inch);
		Length inch63360 = new Length(63360, inch);
		assertTrue(mile1.equals(inch63360));
		assertFalse(mile1.equals(inch12));
	}
}
