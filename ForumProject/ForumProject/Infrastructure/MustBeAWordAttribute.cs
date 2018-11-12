using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumProject.Infrastructure
{
    public class MustBeAWordAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;

            if(value==null)
            {
                return result = false;
            }

            string text = value.ToString();

            for (int i = 0; i < text.Length; i++)
            {

                if (System.Char.IsDigit(text[i]))
                {
                    result = false;
                    return result;
                }

            }

            return result;


        }


    }
}