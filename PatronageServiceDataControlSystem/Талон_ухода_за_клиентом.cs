namespace PatronageServiceDataControlSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Талон_ухода_за_клиентом")]
    public partial class Талон_ухода_за_клиентом
    {
        [Key]
        [Column("ID_талона")]
        public int ID_талона { get; set; }

        [Column("ID_сотрудника")]
        public int ID_сотрудника { get; set; }

        [Column("ID_клиента")]
        public int ID_клиента { get; set; }

        [Column("ID_услуги")]
        public int ID_услуги { get; set; }

        [Range(typeof(DateTime), "2019-01-01", "2024-01-01")]
        [Column("Дата_начала_обслуживания", TypeName = "date")]
        public DateTime Дата_начала_обслуживания { get; set; }

        [Range(typeof(DateTime), "2019-01-01", "2024-01-01")]
        [Column("Дата_окончания_обслуживания", TypeName = "date")]
        public DateTime Дата_окончания_обслуживания { get; set; }
               

        [Column("Схема_мероприятий")]
        [Required]
        [StringLength(100)]
        public string Схема_мероприятий { get; set; }

        [Column("Медицинские_показания")]
        [StringLength(50)]
        public string Медицинские_показания { get; set; }

        [Column("Сумма_к_оплате", TypeName = "money")]
        public decimal? Сумма_к_оплате { get; set; }

        [Range(0, 1)]
        [Required]
        public double? Скидка { get; set; }

        public virtual Клиент Клиент { get; set; }

        public virtual Сотрудник Сотрудник { get; set; }

        public virtual Услуга Услуга { get; set; }
    }
}
