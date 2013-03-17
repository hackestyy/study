///////////////////////////////////////////////////////////
//  Component.cs
//  Implementation of the Class Component
//  Generated by Enterprise Architect
//  Created on:      19-十二月-2011 17:06:45
//  Original author: zhanghao
///////////////////////////////////////////////////////////




namespace ZteApp.ProductService.Core {
	public abstract class Component {

		public Component(){

		}

		~Component(){

		}

		/// 
		/// <param name="component"></param>
		public abstract void Add(Component component);

		protected abstract void DoWork();

		public abstract void Initialize();

		/// 
		/// <param name="component"></param>
		public abstract void Remove(Component component);

		public abstract void Work();

	}//end Component

}//end namespace Core