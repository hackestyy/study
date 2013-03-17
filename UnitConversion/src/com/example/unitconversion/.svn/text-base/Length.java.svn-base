package com.example.unitconversion;

public class Length {
	private int size;
	
	private LengthUnit lu;
	public Length(int size,LengthUnit lu){
		this.size = size;
		this.lu = lu;
	}
	public int getSize() {
		return size;
	}
	public LengthUnit getLu() {
		return lu;
	}
	public boolean equals(Object anObject)
	{
		 if (this == anObject) {
		     return true;
		 }
		 
		 if (anObject instanceof Length) {
			 Length anotherLength = (Length)anObject;
			 if (size*lu.GetNum2Base()==anotherLength.size*anotherLength.lu.GetNum2Base()) {
				return true;
			}
		 }
		
	     return false;
	}
	
}
