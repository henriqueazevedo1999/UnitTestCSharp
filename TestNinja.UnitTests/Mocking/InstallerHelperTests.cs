using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader;
    private InstallerHelper _installerHelper;

    [SetUp]
    public void SetUp()
    {
        _fileDownloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(_fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadSucceed_ReturnTrue()
    {
        // Act
        var result = _installerHelper.DownloadInstaller("customer", "installer");

        // Assert 
        Assert.True(result);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        // Arrange
        _fileDownloader.Setup(f => f.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

        // Act
        var result = _installerHelper.DownloadInstaller("customer", "installer");

        // Assert 
        Assert.IsFalse(result);
    }
}
