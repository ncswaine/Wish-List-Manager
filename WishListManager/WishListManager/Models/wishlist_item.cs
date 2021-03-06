//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WishListManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class wishlist_item
    {
        public int id { get; set; }

        [Required]
        [Display(Name ="Name")]
        public int person_id { get; set; }

        [Required(ErrorMessage ="Please enter a description.")]
        [StringLength(200,ErrorMessage ="Description cannot exceed 200 characters.")]
        [Display(Name = "Description")]
        public string description { get; set; }   
        
        [Display(Name = "Type")]     
        public string type { get; set; }

        [Display(Name = "Purchased?")]
        public bool is_purchased { get; set; }
        public bool is_deleted { get; set; }
    
        public virtual person person { get; set; }
    }
}
