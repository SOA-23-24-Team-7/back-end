using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests.Integration;

[Collection("Sequential")]
public class PaymentQueryTests : BasePaymentsIntegrationTest
{
    public PaymentQueryTests(PaymentsTestFactory factory) : base(factory) { }

    [Fact]
    public void Retreives_all()
    {

    }
}
