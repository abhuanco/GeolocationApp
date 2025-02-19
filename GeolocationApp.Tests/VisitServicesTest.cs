using GeolocationApp.Application.Interfaces;
using GeolocationApp.Domain.DTOs;
using Moq;

namespace GeolocationApp.Tests;

public class VisitServicesTest
{
    private readonly Mock<IVisitService> _visitServiceMock;

    public VisitServicesTest()
    {
        _visitServiceMock = new Mock<IVisitService>();
    }

    [Fact]
    public async Task GetVisitByIdAsync_ShouldReturnVisit_WhenIdExists()
    {
        // Arrange
        var visitId = Guid.NewGuid();
        var expectedVisit = new ResponseVisit
        {
            Id = visitId,
            Country = "Bolivia",
            Currency = "BOB",
            VisitDate = DateTime.UtcNow
        };

        _visitServiceMock
            .Setup(service => service.GetVisitByIdAsync(visitId))
            .ReturnsAsync(expectedVisit);

        // Act
        var result = await _visitServiceMock.Object.GetVisitByIdAsync(visitId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(visitId, result!.Id);
        Assert.Equal("Bolivia", result.Country);
    }

    [Fact]
    public async Task GetVisitByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        var visitId = Guid.NewGuid();

        _visitServiceMock
            .Setup(service => service.GetVisitByIdAsync(visitId))
            .ReturnsAsync((ResponseVisit?)null);

        // Act
        var result = await _visitServiceMock.Object.GetVisitByIdAsync(visitId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateVisitAsync_ShouldReturnVisit_WhenDataIsValid()
    {
        // Arrange
        var visitRequest = new UpdateVisit
        {
            Country = "Bolivia",
            Currency = "BOB",
            VisitDate = DateTime.UtcNow
        };

        var expectedVisit = new ResponseVisit
        {
            Id = Guid.NewGuid(),
            Country = "Bolivia",
            Currency = "BOB",
            VisitDate = visitRequest.VisitDate
        };

        _visitServiceMock
            .Setup(service => service.CreateVisitAsync(visitRequest))
            .ReturnsAsync(expectedVisit);

        // Act
        var result = await _visitServiceMock.Object.CreateVisitAsync(visitRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bolivia", result.Country);
        Assert.Equal("BOB", result.Currency);
    }

    [Fact]
    public async Task UpdateVisitAsync_ShouldReturnUpdatedVisit_WhenIdAndDataAreValid()
    {
        // Arrange
        var visitId = Guid.NewGuid();
        var visitRequest = new UpdateVisit
        {
            Country = "Argentina",
            Currency = "ARS",
            VisitDate = DateTime.UtcNow
        };

        var updatedVisit = new ResponseVisit
        {
            Id = visitId,
            Country = "Argentina",
            Currency = "ARS",
            VisitDate = visitRequest.VisitDate
        };

        _visitServiceMock
            .Setup(service => service.UpdateVisitAsync(visitId, visitRequest))
            .ReturnsAsync(updatedVisit);

        // Act
        var result = await _visitServiceMock.Object.UpdateVisitAsync(visitId, visitRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Argentina", result!.Country);
        Assert.Equal("ARS", result.Currency);
    }

    [Fact]
    public async Task DeleteVisitAsync_ShouldReturnTrue_WhenIdExists()
    {
        // Arrange
        var visitId = Guid.NewGuid();

        _visitServiceMock
            .Setup(service => service.DeleteVisitAsync(visitId))
            .ReturnsAsync(true);

        // Act
        var result = await _visitServiceMock.Object.DeleteVisitAsync(visitId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetPagedVisitsAsync_ShouldReturnPagedResults_WhenCalledWithValidParams()
    {
        // Arrange
        int pageIndex = 0;
        int pageSize = 2;

        var expectedVisits = new List<ResponseVisit>
        {
            new ResponseVisit { Country = "Bolivia", Currency = "BOB", VisitDate = DateTime.UtcNow },
            new ResponseVisit { Country = "Argentina", Currency = "ARS", VisitDate = DateTime.UtcNow.AddDays(-1) }
        };

        _visitServiceMock
            .Setup(service => service.GetPagedVisitsAsync(pageIndex, pageSize, null, null))
            .ReturnsAsync((expectedVisits, 10)); // Total count is 10 for pagination.

        // Act
        var (data, totalCount) = await _visitServiceMock.Object.GetPagedVisitsAsync(pageIndex, pageSize);

        // Assert
        Assert.NotNull(data);
        Assert.Equal(2, data.Count());
        Assert.Equal(10, totalCount);
    }
}