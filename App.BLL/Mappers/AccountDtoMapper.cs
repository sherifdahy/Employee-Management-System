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
        public static AccountDTO ToDTO(this Account account)
        {
            if (account == null) return null;

            return new AccountDTO()
            {
                Id = account.Id,
                Currency = account.Currency,
            };
        }
        #endregion


        #region DTO => Model
        public static Account ToModel(this AccountDTO accountDTO)
        {
            if (accountDTO == null) return null;

            return new Account()
            {
                Id = accountDTO.Id,
                Currency = accountDTO.Currency,
            };
        }

        #endregion
    }
}
