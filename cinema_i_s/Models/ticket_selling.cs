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
    
    public partial class ticket_selling
    {
        public int id_ticket { get; set; }
        public System.DateTime date_of_sale { get; set; }
        public int associate_id { get; set; }
        public int ticket_selling_id { get; set; }
    
        public virtual associate associate { get; set; }
        public virtual ticket ticket { get; set; }
    }
}