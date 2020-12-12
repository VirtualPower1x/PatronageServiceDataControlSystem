namespace PatronageServiceDataControlSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Клиент
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиент()
        {
            Талон_ухода_за_клиентом = new HashSet<Талон_ухода_за_клиентом>();
        }

        [Key]
        [Column("ID_клиента")]
        public int ID_клиента { get; set; }

        [Column("Фамилия_клиента")]
        [Required]
        [StringLength(50)]
        public string Фамилия_клиента { get; set; }

        [Column("Имя_клиента")]
        [Required]
        [StringLength(50)]
        public string Имя_клиента { get; set; }

        [Column("Пол_клиента")]
        [Required]
        [StringLength(50)]
        public string Пол_клиента { get; set; }

        [Range(typeof(DateTime), "1919-01-01", "2001-06-01")]
        [Column("Дата_рождения_клиента", TypeName = "date")]
        public DateTime Дата_рождения_клиента { get; set; }

        [Column("Адрес_клиента")]
        [Required]
        [StringLength(50)]
        public string Адрес_клиента { get; set; }

        [Column("Телефон_клиента")]
        [Required]
        [StringLength(50)]
        public string Телефон_клиента { get; set; }

        [StringLength(50)]
        public string Заболевание { get; set; }

        [Column("Категория_инвалидности")]
        [StringLength(50)]
        public string Категория_инвалидности { get; set; }

        [Column("Постоянный_уход")]
        public bool Постоянный_уход { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Талон_ухода_за_клиентом> Талон_ухода_за_клиентом { get; set; }
    }
}
