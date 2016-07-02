﻿using System;

namespace Voucherify.Client
{
    public class VoucherifyClient
    {
        public string AppToken { get; private set; }
        public string AppId { get; private set; }
        public string Endpoint { get; private set; }
        public bool Secure { get; private set; } 

        public Api.Vouchers Vouchers { get; private set; }
        public Api.Redemptions Redemptions { get; private set; }
        public Api.Customers Customers { get; private set; }

        public VoucherifyClient(string appId, string appToken)
        {
            if (string.IsNullOrEmpty(appToken))
            {
                throw new ArgumentNullException("appToken");
            }

            if (string.IsNullOrEmpty("appId"))
            {
                throw new ArgumentNullException("appId");
            }

            this.AppToken = appToken;
            this.AppId = appId;
            this.Secure = true;
            this.Endpoint = Constants.EndpointApi;

            this.Vouchers = new Api.Vouchers(this);
            this.Redemptions = new Api.Redemptions(this);
            this.Customers = new Api.Customers(this);
        }

        public VoucherifyClient WithSSL()
        {
            this.Secure = true;
            return this;
        }

        public VoucherifyClient WithoutSSL()
        {
            this.Secure = false;
            return this;
        }

        public VoucherifyClient WithEndpoint(string endpoint)
        {
            this.Endpoint = endpoint;

            if (endpoint == null)
            {
                this.Endpoint = Constants.EndpointApi;
            }

            return this;
        }
    }
}
