namespace TestNinja.Mocking;

public class HousekeeperService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IXtraMessageBox _xtraMessageBox;
    private readonly IEmailSender _emailSender;
    private readonly IStatementGenerator _statementGenerator;

    public HousekeeperService(IUnitOfWork unitOfWork, IXtraMessageBox xtraMessageBox, IEmailSender emailSender, IStatementGenerator statementGenerator)
    {
        _unitOfWork = unitOfWork;
        _xtraMessageBox = xtraMessageBox;
        _emailSender = emailSender;
        _statementGenerator = statementGenerator;
    }

    public void SendStatementEmails(DateTime statementDate)
    {
        var housekeepers = _unitOfWork.Query<Housekeeper>();

        foreach (var housekeeper in housekeepers)
        {
            if (string.IsNullOrWhiteSpace(housekeeper.Email))
                continue;

            var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

            if (string.IsNullOrWhiteSpace(statementFilename))
                continue;

            var emailAddress = housekeeper.Email;
            var emailBody = housekeeper.StatementEmailBody;

            try
            {
                _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                    string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
            }
            catch (Exception e)
            {
                _xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                    MessageBoxButtons.OK);
            }
        }
    }
}

public class MainForm
{
    public bool HousekeeperStatementsSending { get; set; }
}

public class DateForm
{
    public DateForm(string statementDate, object endOfLastMonth)
    {
    }

    public DateTime Date { get; set; }

    public DialogResult ShowDialog()
    {
        return DialogResult.Abort;
    }
}

public enum DialogResult
{
    Abort,
    OK
}

public class SystemSettingsHelper
{
    public static string EmailSmtpHost { get; set; }
    public static int EmailPort { get; set; }
    public static string EmailUsername { get; set; }
    public static string EmailPassword { get; set; }
    public static string EmailFromEmail { get; set; }
    public static string EmailFromName { get; set; }
}

public class Housekeeper
{
    public string Email { get; set; }
    public int Oid { get; set; }
    public string FullName { get; set; }
    public string StatementEmailBody { get; set; }
}
