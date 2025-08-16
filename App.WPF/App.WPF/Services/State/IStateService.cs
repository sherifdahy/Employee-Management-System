using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WPF.Services.State
{
    public interface IStateService
    {
        public Guid UserId { get; set; }
    }
}
