//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models.Agency
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract
    {
        public int id { get; set; }
        public string series { get; set; }
        public Nullable<int> number { get; set; }
        public int insurant { get; set; }
        public int agent { get; set; }
        public int tax { get; set; }
        public System.DateTime date { get; set; }
        public int coef { get; set; }
        public Nullable<decimal> cost { get; set; }
        public int status { get; set; }
    
        public virtual Agent Agent1 { get; set; }
        public virtual Coef Coef1 { get; set; }
        public virtual Insurant Insurant1 { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual Tax Tax1 { get; set; }
    }
}
