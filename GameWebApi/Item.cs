using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi
{

    public enum ItemType
    {
        SWORD,
        POTION,
        SHIELD
    }
    public class Item
    {
        DateTime localDate = DateTime.UtcNow;
        
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Range(1, 99, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Level { get; set; }

        [EnumDataType(typeof(ItemType))]
        public ItemType Type { get; set; }



        [ValidTime]
        public DateTime CreationTime { get; set; }
    }


    public class ValidTime : ValidationAttribute
    {
        public DateTime CreationTime { get; }

        public string GetErrorMessage() => $"Date is from the future"; 
        //Not 100% sure from the assignment whether the error is supposed to be raised if the creation time is higher or lower than the current time. So future/past.
        // I chose to interpret it as "Items with their creation date in the future are invalid".


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var now = DateTime.UtcNow;

            if (now < CreationTime)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
