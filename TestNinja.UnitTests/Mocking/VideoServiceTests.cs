using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> fileReader;
    private Mock<IVideoRepository> videoRepository;
    private VideoService videoService;

    [SetUp]
    public void SetUp()
    {
        fileReader = new Mock<IFileReader>();
        videoRepository = new Mock<IVideoRepository>();
        videoService = new VideoService(fileReader.Object, videoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
    {
        // Arrange
        fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        // Act
        var result = videoService.ReadVideoTitle();

        // Assert 
        Assert.That(result, Does.Contain("Error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnsAStringOfIdsOfUnprocessedVideos()
    {
        // Arrange
        videoRepository.Setup(v => v.GetUnprocessedVideos()).Returns(new Video[]
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

        // Act
        var result = videoService.GetUnprocessedVideosAsCsv();

        // Assert 
        Assert.That(result, Is.EqualTo("1,2,3"));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsAnEmptyString()
    {
        // Arrange
        videoRepository.Setup(v => v.GetUnprocessedVideos())
            .Returns(new Video[] { });

        // Act
        var result = videoService.GetUnprocessedVideosAsCsv();

        // Assert 
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_HasOnly1UnprocessedVideo_ReturnsId()
    {
        // Arrange
        videoRepository.Setup(v => v.GetUnprocessedVideos())
            .Returns(new Video[] { new Video { Id = 1 } });

        // Act
        var result = videoService.GetUnprocessedVideosAsCsv();

        // Assert 
        Assert.That(result, Is.EqualTo("1"));
    }
}
