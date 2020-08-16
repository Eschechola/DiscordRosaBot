namespace RosaBot.Commands.Models
{
    public class Quotation
    {
        public string Code { get; set; }

        public string CodeIn { get; set; }

        public string Name { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Ask { get; set; }

        public double Bid { get; set; }

        public string VarBid { get; set; }

        public string TimeStamp { get; set; }

        public string CreateDate { get; set; }

        public string PctChange { get; set; }
    }
}
