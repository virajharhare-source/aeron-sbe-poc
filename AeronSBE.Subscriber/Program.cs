using Adaptive.Aeron;
using AeronSBE.Messages;

namespace AeronSBE.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new Aeron.Context();
            using var aeron = Aeron.Connect(ctx);

            const string channel = "aeron:udp?endpoint=localhost:40123";
            const int streamId = 1001;

            using var subscription = aeron.AddSubscription(channel, streamId);
            Console.WriteLine("Subscriber listening... Ctrl+C to exit.");

            var fragmentAssembler = new FragmentAssembler((buffer, offset, length, header) =>
            {
                var msgHeader = new MessageHeaderDecoder().Wrap(buffer, offset);
                if (msgHeader.TemplateId == OrderEncoder.TEMPLATE_ID)
                {
                    var order = new OrderDecoder().Wrap(buffer, offset + MessageHeaderEncoder.ENCODED_LENGTH);
                    Console.WriteLine($"Received OrderId={order.OrderId}, Price={order.Price}, Quantity={order.Quantity}");
                }
            });

            while (true)
            {
                subscription.Poll(fragmentAssembler.OnFragment, 10);
            }
        }
    }
}
