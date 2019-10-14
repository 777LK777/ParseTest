using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib.MODEL.Work
{
    public class BaseExchanger
    {
        /// <summary>
        /// Время снятия курса
        /// </summary>
        public string TimeOfReceipt { get; set; } = string.Empty;

        /// <summary>
        /// Имя обменника
        /// </summary>
        public string ExchangerName { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на обменник
        /// </summary>
        public string ExchangerUri { get; set; } = string.Empty;

        /// <summary>
        /// Цена альткоина
        /// </summary>
        public double SellPrice { get; set; } = 0;

        /// <summary>
        /// Имя альткоина
        /// </summary>
        public string AltcoinName { get; set; } = string.Empty;

        /// <summary>
        /// Минимальная сумма для обмена
        /// </summary>
        public double From { get; set; } = 0;

        /// <summary>
        /// Максимальная сумма для обмена
        /// </summary>
        public double? To { get; set; } = null;

        /// <summary>
        /// Валютный резерв обменника
        /// </summary>
        public double Reserve { get; set; } = 0;
    }
}
