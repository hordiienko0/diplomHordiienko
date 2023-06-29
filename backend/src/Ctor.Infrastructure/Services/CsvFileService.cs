using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Users.Commands;
using Ctor.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Ctor.Infrastructure.Services;
public class CsvFileService : ICsvFileService
{
    public List<string> ErrorLines { get; private set; }
    public List<UserCsvDto> ReadMembers(Stream fileStream)
    {
        var models = new List<UserCsvDto>();
        ErrorLines = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim
        };
        using (var reader = new StreamReader(fileStream))
        using (var csv = new CsvReader(reader, config))
        {
            while (csv.Read())
            {
                string currentLine = csv.Parser.RawRecord.TrimEnd(new char[] { '\n', '\r' });
                try
                {
                    var model = UserParser(csv);
                    UserCsvDtoValidator validatior = new UserCsvDtoValidator();
                    var result = validatior.Validate(model);
                    if (result.IsValid)
                        models.Add(model);
                    else
                        ErrorLines.Add(currentLine);
                }
                catch
                {
                    ErrorLines.Add(currentLine);
                }
            }
        }
        return models;
    }
    UserCsvDto UserParser(CsvReader csv)
    {
        return new UserCsvDto
        {
            FirstName=csv.GetField(0),
            LastName=csv.GetField(1),
            RoleName=csv.GetField(2),
            UserEmail=csv.GetField(3)
        };
    }
}
