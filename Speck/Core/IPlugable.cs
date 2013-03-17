///////////////////////////////////////////////////////////
//  IPlugable.cs
//  Implementation of the Interface IPlugable
//  Generated by Enterprise Architect
//  Created on:      14-十二月-2011 16:22:52
//  Original author: zhanghao
///////////////////////////////////////////////////////////

using System;


namespace ZteApp.ProductService.Core {
	public interface IPlugable {

		void PlugIn();

		void PlugOff();
	}//end IPlugable

    public delegate void PlugInEventHandler(object sender,PlugEventArgs e);
    public delegate void PlugOffEventHandler(object sender,PlugEventArgs e);

}//end namespace Core