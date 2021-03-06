﻿using System;
using SmartCore.Entities.General.Interfaces;

namespace SmartCore.Entities.Dictionaries
{
    /// <summary>
    /// Описвыает объеты, представляющие описание режима работы агрегатов
    /// </summary>
    public interface IWorkRegime : IBaseEntityObject
    {
        #region String ShortName { get; set; }
        /// <summary>
        /// Короткое имя
        /// </summary>
        String ShortName { get; set; }

        #endregion

        #region String FullName { get; set; }
        /// <summary>
        /// Полное имя
        /// </summary>
        String FullName { get; set; }

        #endregion

        #region String CommonName { get; set; }
        /// <summary>
        /// Общее имя 
        /// </summary>
        String CommonName { get; set; }

        #endregion
    }
}
