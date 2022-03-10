using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IXtraMessageBox> _xtraMessageBox;
    private Mock<IEmailSender> _emailSender;
    private Mock<IStatementGenerator> _statementGenerator;
    private HousekeeperService _housekeeperService;

    private Housekeeper houseKeeper;
    private string _statementFileName;
    private readonly DateTime _statementDate = DateTime.Now;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _xtraMessageBox = new Mock<IXtraMessageBox>();
        _emailSender = new Mock<IEmailSender>();
        _statementGenerator = new Mock<IStatementGenerator>();
        _housekeeperService = new HousekeeperService(_unitOfWork.Object, _xtraMessageBox.Object, _emailSender.Object, _statementGenerator.Object);

        houseKeeper = new Housekeeper
        {
            Email = "a",
            FullName = "b",
            Oid = 1,
            StatementEmailBody = "d"
        };
        _statementFileName = "FileName";

        _unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper> { houseKeeper }.AsQueryable());
        _statementGenerator.Setup(sg => sg.SaveStatement(houseKeeper.Oid, houseKeeper.FullName, _statementDate)).Returns(() => _statementFileName);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        // Act
        _housekeeperService.SendStatementEmails(_statementDate);

        // Assert 
        VerifyStatementGenerated();
    }

    [Test]
    public void SendStatementEmails_WhenCalled_EmailTheStatement()
    {
        // Act
        _housekeeperService.SendStatementEmails(_statementDate);

        // Assert 
        VerifyEmailSent();
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_EmailIsInvalid_ShouldNotGenerateStatement(string email)
    {
        // Arrange
        houseKeeper.Email = email;

        // Act
        _housekeeperService.SendStatementEmails(_statementDate);

        // Assert 
        VerifyStatementNotGenerated();
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void SendStatementEmails_GeneratedStatementFilenameIsInvalid_ShouldNotSendStatementEmail(string filename)
    {
        // Arrange
        _statementFileName = filename;

        // Act
        _housekeeperService.SendStatementEmails(_statementDate);

        // Assert 
        VerifyEmailNotSent();
    }

    [Test]
    public void SendStatementEmails_EmailSendingFails_ShowMessageBox()
    {
        // Arrange
        _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.IsAny<string>()
            )).Throws<Exception>();

        // Act
        _housekeeperService.SendStatementEmails(_statementDate);

        // Assert 
        _xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
    }

    private void VerifyStatementNotGenerated()
    {
        _statementGenerator.Verify(x => x.SaveStatement(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>()),
            Times.Never);
    }

    private void VerifyEmailNotSent()
    {
        _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    private void VerifyEmailSent()
    {
        _emailSender.Verify(es => es.EmailFile(
                houseKeeper.Email,
                houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()),
            Times.Once);
    }

    private void VerifyStatementGenerated()
    {
        _statementGenerator.Verify(sg => sg.SaveStatement(houseKeeper.Oid, houseKeeper.FullName, _statementDate), Times.Once);
    }
}
