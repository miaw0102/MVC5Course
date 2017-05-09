using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttrbutes
{
    public class 商品名稱必須包含alice字串 : DataTypeAttribute
    {
        public 商品名稱必須包含alice字串():base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            var str =(string)value;
            return str.Contains("alice");
        }
    }
}