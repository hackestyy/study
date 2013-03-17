using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    internal interface ISchduler<T>
    {
        T Get();

        /// <summary>
        /// Cancels the specified scheduled obj, remove it from the queue and put it back to repository.
        /// </summary>
        /// <param name="scheduledObj">The scheduled obj.</param>
        /// <returns>True if succeed cancelled, otherwise false</returns>
        bool Cancel(T scheduledObj);

        /// <summary>
        /// Schdules this instance.
        /// </summary>
        void Schdule();

    }
}
