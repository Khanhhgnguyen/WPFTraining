using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFTrainningCSharp.Model
{
    public class GetEnumObjectDataProvider : ObjectDataProvider
    {
        public object GetEnumValues(Enum enumObj)
        {
            var attribute = enumObj.GetType().GetRuntimeField(enumObj.ToString()).
                GetCustomAttributes(typeof(DisplayAttribute), false).
                SingleOrDefault() as DisplayAttribute;
            return attribute == null ? enumObj.ToString() : attribute.Description;
        }

        public List<object> GetListEnum(Type type)
        {
            var getPriceType = Enum.GetValues(type).OfType<Enum>().Select(GetEnumValues).ToList();
            return getPriceType;
        }
    }
}
