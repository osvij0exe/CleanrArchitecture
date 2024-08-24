﻿using CleanArchitecture.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CleanArchitecture.Application.Users.LoginUser
{
    public record  LogingCommand(string Email,string Password ): ICommand<string>;

}
