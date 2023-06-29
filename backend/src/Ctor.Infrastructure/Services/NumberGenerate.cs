using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;

namespace Ctor.Infrastructure.Services;
public class NumberGenerateSercice: INumberGenerateService
{
    public int GetRandomNumberForId()
    {
        int min = 10000000;
        int max = 99999999;
        Random _rdm = new Random();
        return _rdm.Next(min, max);
    }
}
