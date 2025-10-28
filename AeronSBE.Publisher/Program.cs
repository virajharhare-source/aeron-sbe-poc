using Adaptive.Aeron;
using Adaptive.Agrona.Concurrent;
using AeronSBE.Messages;

namespace AeronSBE.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new Aeron.Context();
            using var aeron = Aeron.Connect(ctx);

            const string channel = "aeron:udp?endpoint=localhost:40123";
            const int streamId = 1001;

            using var publication = aeron.AddPublication(channel, streamId);
            Console.WriteLine("Publisher started... Press Ctrl+C to exit.");

            var buffer = new UnsafeBuffer(new byte[1024]);
            var headerEncoder = new MessageHeaderEncoder();
            var orderEncoder = new OrderEncoder();

            long orderId = 1;

            while (true)
            {
                headerEncoder.Wrap(buffer, 0)
                             .BlockLength((short)OrderEncoder.BLOCK_LENGTH)
                             .TemplateId(OrderEncoder.TEMPLATE_ID)
                             .SchemaId(1)
                             .Version(1);

                orderEncoder.Wrap(buffer, MessageHeaderEncoder.ENCODED_LENGTH)
                            .OrderId(orderId)
                            .Price(100.5)
                            .Quantity(10);

                long result;
                do
                {
                    result = publication.Offer(buffer, 0, MessageHeaderEncoder.ENCODED_LENGTH + OrderEncoder.BLOCK_LENGTH);
                } while (result < 0);

                Console.WriteLine($"Sent Order {orderId}");
                orderId++;
                Thread.Sleep(1000);
            }
        }
    }
}
