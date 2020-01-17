using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Рассчет_Чечкина
{
    class Splav
    {
        /// <summary>
        /// Содержание Cu в 1 сплаве, %
        /// </summary>
        public double Cu1 { get; set; }

        /// <summary>
        /// Содержание Cu в 2 сплаве, %
        /// </summary>
        public double Cu2 { get; set; }

        /// <summary>
        /// Содержание Sn в 1 сплаве, %
        /// </summary>
        public double Sn1 { get; set; }

        /// <summary>
        /// Содержание Sn в 2 сплаве, %
        /// </summary>
        public double Sn2 { get; set; }

        /// <summary>
        /// Содержание Zn в 1 сплаве, %
        /// </summary>
        public double Zn1 { get; set; }

        /// <summary>
        /// Содержание Zn в 2 сплаве, %
        /// </summary>
        public double Zn2 { get; set; }

        /// <summary>
        /// Стоимость 1 кг 1 сплава, усл.ед.
        /// </summary>
        public double St1 { get; set; }

        /// <summary>
        /// Стоимость 1 кг 2 сплава, усл.ед.
        /// </summary>
        public double St2 { get; set; }

        /// <summary>
        /// Кол-во сплава 1 в новом составе, кг
        /// </summary>
        public double Ras_Sp1 { get; set; }

        /// <summary>
        /// Кол-во сплава 2 в новом составе, кг
        /// </summary>
        public double Ras_Sp2 { get; set; }

        /// <summary>
        /// Общая соимость нового сплава, усл.ед.
        /// </summary>
        public double Ras_O_St { get; set; }
    }


}
