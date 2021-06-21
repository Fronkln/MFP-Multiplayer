using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MPModEntityAttribute : Attribute
{
    internal string entName = "";

    public MPModEntityAttribute(string entityName)
    {
        entName = entityName;
    }
}

