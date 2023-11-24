using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests;

public class BasePaymentsIntegrationTest : BaseWebIntegrationTest<PaymentsTestFactory>
{
    public BasePaymentsIntegrationTest(PaymentsTestFactory factory) : base(factory) { }
}
