namespace inclassLottery;

using System.Collections.Concurrent;

class Ticket
{
    public int[] RegTickets { get; set; }
    public int PowerBall { get; set; }
    public Ticket(int[] numbers, int powerBall)
    {
        RegTickets = new int[5];
        RegTickets[0] = numbers[0];
        RegTickets[1] = numbers[1];
        RegTickets[2] = numbers[2];
        RegTickets[3] = numbers[3];
        RegTickets[4] = numbers[4];
        PowerBall = powerBall;
    }
    public Ticket()
    {
        RegTickets = new int[5];
        for (int i = 0; i < 5; i++)
        {
            RegTickets[i] = Random.Shared.Next(1, 70);
        }
        PowerBall = Random.Shared.Next(1, 27);
    }
}
class LotteryPeriod
{
    public Ticket WinningTicket { get; set; }
    public ConcurrentBag<Ticket> SoldTickets { get; set; } = new ConcurrentBag<Ticket>();
    public LotteryPeriod()
    {
        int[] numbers = new int[5] { 1, 2, 3, 4, 5 };
        SetWinningTicket(numbers, 6);
    }
    public void SetWinningTicket(int[] numbers, int powerBall)
    {
        WinningTicket = new Ticket(numbers, powerBall);
    }
}
class LotteryVendor
{
    public LotteryVendor()
    {
    }
    public void SellTickets(LotteryPeriod period, int numberOfTickets)
    {
        for (int i = 0; i < numberOfTickets; i++)
        {
            Ticket ticket = new Ticket();
            period.SoldTickets.Add(ticket);
        }
    }
}
class Program
{

    static void Main(string[] args)
    {
        const int VENDOR_AMOUNT = 3;
        const int ALOTTED_TICKETS = 10_000_000;

        Console.WriteLine($"Hello, Lets sell {(VENDOR_AMOUNT*ALOTTED_TICKETS):N0} tickets!");
        LotteryPeriod period = new LotteryPeriod();
        LotteryVendor[] vendors = new LotteryVendor[VENDOR_AMOUNT];

        for (int i = 0; i < vendors.Length; i++)
            vendors[i] = new LotteryVendor();

        Parallel.ForEach(vendors, v => v.SellTickets(period, ALOTTED_TICKETS));

        Console.WriteLine($"{VENDOR_AMOUNT} vendors sold {period.SoldTickets.Count:N0} tickets!");

        //TODO: 1a) make 3 vendors sell 10M tickets each
        // 1b) 3 vendors sell tickets in parallel
        // 2) Modify Ticket class to be able to judge a winner level
        // 3) Gather statistics on how many winners of each level there are
        // 4) Print out the statistics
        // AFTER 1-4 is working, try to do (GatherStatistics) with Parallel Programming
    }
}
