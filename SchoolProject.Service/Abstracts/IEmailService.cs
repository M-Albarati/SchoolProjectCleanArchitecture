using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IEmailService
    {
        public Task<string> SendEmailAsync(string email,string message, string? reason);
    }
}
