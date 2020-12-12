namespace PatronageServiceDataControlSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Сотрудник
    {        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Сотрудник()
        {
            Талон_ухода_за_клиентом = new HashSet<Талон_ухода_за_клиентом>();
        }
        
        [Key]
        [Column("ID_сотрудника")]
        public int ID_сотрудника { get; set; }

        [Column("Фамилия_сотрудника")]
        [Required]
        [StringLength(50)]
        public string Фамилия_сотрудника { get; set; }

        [Column("Имя_сотрудника")]
        [Required]
        [StringLength(50)]
        public string Имя_сотрудника { get; set; }

        [Required]
        [StringLength(50)]
        public string Пол { get; set; }

        [Range(typeof(DateTime), "1919-01-01", "2001-06-01")]
        [Column("Дата_рождения", TypeName = "date")]
        public DateTime Дата_рождения { get; set; }

        [Column("Домашний_адрес_сотрудника")]
        [Required]
        [StringLength(50)]
        public string Домашний_адрес_сотрудника { get; set; }

        [Column("Телефон_сотрудника")]
        [Required]
        [StringLength(50)]
        public string Телефон_сотрудника { get; set; }

        [Range(typeof(DateTime), "2019-01-01", "2019-06-22")]
        [Column("Дата_приема_на_работу", TypeName = "date")]
        public DateTime Дата_приема_на_работу { get; set; }

        [Required]
        [StringLength(50)]
        public string Должность { get; set; }

        [Required]
        [StringLength(50)]
        public string Образование { get; set; }

        [Column("Стаж_работы")]
        [Range (0, 100)]
        public int Стаж_работы { get; set; }

        [Column("Заработная_плата", TypeName = "money")]
        [Range(10000, 200000)]
        public decimal Заработная_плата { get; set; }

        [StringLength(100)]
        public string Специализация { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Талон_ухода_за_клиентом> Талон_ухода_за_клиентом { get; set; }

    }
}

