namespace DownloadVideoYouTobe.Modals
{
    public class ResultModel<T>
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public T Data { get; set; }

        public ResultModel(string Status, string Description, int Amount, T Data)
        {
            this.Status = Status;
            this.Description = Description;
            this.Amount = Amount;
            this.Data = Data;
        }
    }

    public class ErrorModel
    {
        public string ErrorTitle { get; set; }
        public string ErrorDescription { get; set; }
        public ErrorModel(string ErrorTitle, string ErrorDescription)
        {
            this.ErrorTitle = ErrorTitle;
            this.ErrorDescription = ErrorDescription;
        }
    }
}
