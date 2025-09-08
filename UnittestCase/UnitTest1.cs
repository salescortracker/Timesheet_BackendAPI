using Moq;
using DataAccess_Layer.Repository.Audit;
using DataAccess_Layer.Services.Audit;
using DataAccess_Layer.Models;
namespace UnittestCase
{
    public class UnitTest1
    {
        public class AuditServiceTests
        {
            private readonly Mock<IAuditRepository> _repoMock;
            private readonly AuditService _service;

            public AuditServiceTests()
            {
                _repoMock = new Mock<IAuditRepository>();
                _service = new AuditService(_repoMock.Object);
            }

            [Fact]
            public async Task GetAllAsync_ReturnsAudits()
            {
                // Arrange
                var audits = new List<Auditlog>
            {
                new Auditlog { AuditId = 1, UserId = 100, ActionType = "Create", ActionDetails = "Created X" }
            };
                _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(audits);

                // Act
                var result = await _service.GetAllAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Single(result);
            }

            [Fact]
            public async Task GetAsync_WhenFound_ReturnsAudit()
            {
                // Arrange
                var audit = new Auditlog { AuditId = 5, UserId = 200, ActionType = "Update" };
                _repoMock.Setup(r => r.GetAsync(5)).ReturnsAsync(audit);

                // Act
                var result = await _service.GetAsync(5);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(5, result!.AuditId);
            }

            [Fact]
            public async Task CreateAsync_SetsCreatedAt_AndCallsRepo()
            {
                _repoMock.Setup(r => r.AddAsync(It.IsAny<Auditlog>())).Returns(Task.CompletedTask);
                _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

                var sut = new AuditService(_repoMock.Object);
                await sut.CreateAsync(new Auditlog { UserId = 1, ActionType = "Create", CreatedBy = 1 });

                _repoMock.Verify(r => r.AddAsync(It.IsAny<Auditlog>()), Times.Once);
                _repoMock.Verify(r => r.SaveAsync(), Times.Once);  // must match service call exactly
            }

            [Fact]
            public async Task UpdateAsync_WhenAuditExists_ReturnsTrue()
            {
                // Arrange
                var existing = new Auditlog { AuditId = 1, UserId = 100, ActionType = "Old" };

                _repoMock.Setup(r => r.GetAsync(1)).ReturnsAsync(existing);
                _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Auditlog>())).Returns(Task.CompletedTask);
                _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

                var dto = new Auditlog { AuditId = 1, UserId = 101, ActionType = "Updated" };

                // Act
                var result = await _service.UpdateAsync(dto);   // await it

                // Assert
                Assert.True(result);
                Assert.Equal(101, existing.UserId);
                _repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
                _repoMock.Verify(r => r.SaveAsync(), Times.Once);
            }

            [Fact]
            public async Task UpdateAsync_WhenNotFound_ReturnsFalse()
            {
                _repoMock.Setup(r => r.GetAsync(1)).ReturnsAsync(new Auditlog { AuditId = 1 });
                _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Auditlog>())).Returns(Task.CompletedTask);
                _repoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

                var result = await _service.UpdateAsync(new Auditlog { AuditId = 1, UserId = 42 });
                Assert.True(result);

                _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Auditlog>()), Times.Once);
                _repoMock.Verify(r => r.SaveAsync(), Times.Once);
            }

            [Fact]
            public async Task DeleteAsync_CallsRepo()
            {
                _repoMock.Setup(r => r.GetAsync(It.IsAny<int>()))
           .ReturnsAsync(new Auditlog { AuditId = 3 });
                _repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                         .Returns(Task.CompletedTask);
                _repoMock.Setup(r => r.SaveAsync())
                         .Returns(Task.CompletedTask);

                var sut = new AuditService(_repoMock.Object);
                var result = await sut.DeleteAsync(3);

                Assert.True(result);
                _repoMock.Verify(r => r.DeleteAsync(3), Times.Once);
                _repoMock.Verify(r => r.SaveAsync(), Times.Once);   // must match service call
            }
        }
    }
}