﻿using System;

namespace Poc.Application.Model
{
    public class UserTokenModel
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}