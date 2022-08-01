using System;


#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class BillPay
    {
        public void BlockBillPay()
        {
            Blocked = true;
            ModifyDate = DateTime.UtcNow;
        }
        public void UnBlockBillPay()
        {
            Blocked = false;
            ModifyDate = DateTime.UtcNow;
        }
    }
}
