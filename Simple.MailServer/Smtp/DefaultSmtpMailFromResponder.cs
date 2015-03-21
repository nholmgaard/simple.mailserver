﻿#region Header
// Copyright (c) 2013-2015 Hans Wolff
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using System;
using Simple.MailServer.Mime;
using Simple.MailServer.Smtp.Config;

namespace Simple.MailServer.Smtp
{
    public class DefaultSmtpMailFromResponder<T> : IRespondToSmtpMailFrom where T : IConfiguredSmtpRestrictions
    {
        protected readonly T Configuration;
        private readonly IEmailValidator _emailValidator;

        public DefaultSmtpMailFromResponder(T configuration, IEmailValidator emailValidator)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (emailValidator == null) throw new ArgumentNullException("emailValidator");

            Configuration = configuration;
            _emailValidator = emailValidator;
        }

        public SmtpResponse VerifyMailFrom(SmtpSessionInfo sessionInfo, MailAddressWithParameters mailAddressWithParameters)
        {
            if (!_emailValidator.Validate(mailAddressWithParameters.MailAddress))
                return SmtpResponse.SyntaxError;

            return SmtpResponse.OK;
        }
    }
}
