//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cinema_i_s.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class seance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public seance()
        {
            this.ticket = new HashSet<ticket>();
        }
    
        public int id_film { get; set; }
        public int id_assiciate { get; set; }
        public System.DateTime date { get; set; }
        public int seance_id { get; set; }
        public int id_hall { get; set; }
        public string price { get; set; }
        public string movie_format { get; set; }
    
        public virtual associate associate { get; set; }
        public virtual film film { get; set; }
        public virtual hall hall { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ticket> ticket { get; set; }
    }
}
