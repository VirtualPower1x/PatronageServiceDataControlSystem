namespace PatronageServiceDataControlSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Услуга
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Услуга()
        {
            Талон_ухода_за_клиентом = new HashSet<Талон_ухода_за_клиентом>();
        }

        [Key]
        [Column("ID_услуги")]
        public int ID_услуги { get; set; }

        [Column("Название_услуги")]
        [Required]
        [StringLength(50)]
        public string Название_услуги { get; set; }

        [Column("Описание_услуги")]
        [StringLength(50)]
        public string Описание_услуги { get; set; }

        [Column("Цена_услуги", TypeName = "money")]
        public decimal Цена_услуги { get; set; }

        [Column("Максимальная_скидка")]
        [Range(0, 1)]
        public double Максимальная_скидка { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Талон_ухода_за_клиентом> Талон_ухода_за_клиентом { get; set; }
    }
}
