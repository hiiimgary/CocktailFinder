//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CocktailFinder.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cocktail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cocktail()
        {
            this.connect_ingredient_cocktail = new HashSet<connect_ingredient_cocktail>();
            this.connect_user_cocktail = new HashSet<connect_user_cocktail>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Taste { get; set; }
        public string Occasion { get; set; }
        public string Recipe { get; set; }
        public string Description { get; set; }
        public byte[] img { get; set; }
        public string Embed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connect_ingredient_cocktail> connect_ingredient_cocktail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<connect_user_cocktail> connect_user_cocktail { get; set; }
    }
}
