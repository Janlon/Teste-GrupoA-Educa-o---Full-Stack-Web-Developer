using System;

namespace CustomValidation
{
    internal class CustomValidationCPFAttribute : Attribute
    {
        public CustomValidationCPFAttribute() { }

        public string ErrorMessage { get; set; }

        public bool IsValid(object value)
        {
            if (value != null || !string.IsNullOrEmpty(value.ToString())){
                if(!value.Equals(1)){
                    return true;
                }
            }
                
            return false;    
        }
    }
}