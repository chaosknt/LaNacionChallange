using System.ComponentModel.DataAnnotations;

namespace LaNacion.Entities.Enums
{
    public enum LocationType
    {
        [Display(Name = "State")]
        State = 1,

        [Display(Name = "City")]
        City = 2 
    }
}
