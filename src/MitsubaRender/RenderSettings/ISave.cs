using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MitsubaRender.Integrators
{
    public interface ISave
    {
        /// <summary>
        /// Save this object with the Type name.
        /// </summary>
        /// <returns>True if saved successfully</returns>
        bool Save();

        /// <summary>
        /// Save this object with the given name, if name is null or empty it will be .
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True if saved successfully</returns>
        bool Save(String name);

    }
}
