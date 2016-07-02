﻿using System;
using System.Collections.Generic;
using Voucherify.Client.Extensions;

namespace Voucherify.Client.Api
{
    public class Redemptions
    {
        private ApiClient client;

        public Redemptions(VoucherifyClient voucherify)
        {
            if (voucherify == null)
            {
                throw new ArgumentNullException("voucherify");
            }

            this.client = new ApiClient(voucherify);
        }

        public IList<DataModel.RedemptionDetails> ListRedemptions(DataModel.RedemptionsFilter filter)
        {
            UriBuilder uriBuilder = this.client.GetUriBuilder("/redemptions").WithQuery(filter);
            return this.client.DoGetRequest<IList<DataModel.RedemptionDetails>>(uriBuilder.Uri);
        }

        public DataModel.VoucherRedemptionResult RollbackRedemption(string redemptionId, DataModel.RedemptionRollbackQuery query)
        {
            UriBuilder uriBuilder = this.client.GetUriBuilder(string.Format("/redemptions/{0}/rollback", redemptionId)).WithQuery(query);
            return this.client.DoPostRequest<DataModel.VoucherRedemptionResult>(uriBuilder.Uri);
        }
    }
}
