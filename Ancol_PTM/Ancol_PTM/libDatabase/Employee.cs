//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ancol_PTM.libDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.ProjectTasks = new HashSet<ProjectTask>();
        }
    


        public System.Guid Id { get; set; }
        [Required(ErrorMessage ="Field is required")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "The First Word Must Be On uppercase")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "The First Word Must Be On uppercase")]
        [Required(ErrorMessage ="Field is required")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",ErrorMessage = "Entered phone format is not valid.")]
        [DisplayName("Contact no")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage ="Email Type")]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Skills")]
        public string Skills { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> InsAt { get; set; }
        public string InsBy { get; set; }
        public Nullable<System.DateTime> UpdAt { get; set; }
        public string UpdBy { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " "+ LastName;
            }
        }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
