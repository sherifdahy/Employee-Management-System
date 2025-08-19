using App.BLL.DTOs;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Mappers
{
    public static class AccountDtoMapper
    {
        #region Model => DTO
        public static void ToDTO(this Account account,AccountDTO accountDTO)
        {
            if (account == null || account is null) return;

            accountDTO.Currency = account.Currency;
        }
        #endregion


        #region DTO => Model
        public static void ToModel(this AccountDTO accountDTO,Account account)
        {
            if (accountDTO == null || account is null) return;

            account.Currency = accountDTO.Currency;
        }

        #endregion
    }
}
