using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgerClan.Maui.Services;

public interface IApiService
{
    public Task SetStrategyAsync(string apiUrl, string strategyChoice);
}
