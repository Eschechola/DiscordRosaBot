namespace RosaBot.Domain.Entities
{
    public class Quotation
    {
        public string Code { get; protected set; }

        public string CodeIn { get; protected set; }

        public string Name { get; protected set; }

        public string High { get; protected set; }

        public string Low { get; protected set; }

        public string Ask { get; protected set; }

        public string Bid { get; protected set; }

        public string VarBid { get; protected set; }

        public string TimeStamp { get; protected set; }

        public string CreateDate { get; protected set; }

        public string PctChange { get; protected set; }

        protected Quotation()
        {
        }

        public Quotation(
            string code,
            string codeIn,
            string name,
            string high,
            string low,
            string ask,
            string bid,
            string varBid,
            string timeStamp,
            string createDate,
            string pctChange)
        {
            Code = code;
            CodeIn = codeIn;
            Name = name;
            High = high;
            Low = low;
            Ask = ask;
            Bid = bid;
            VarBid = varBid;
            TimeStamp = timeStamp;
            CreateDate = createDate;
            PctChange = pctChange;
        }
    }
}
